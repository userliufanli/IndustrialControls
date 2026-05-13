using System;
using System.Threading;
using System.Threading.Tasks;

namespace IndustrialControls.Utilities
{
    /// <summary>
    /// 重试策略配置
    /// </summary>
    public class RetryPolicy
    {
        public int MaxRetryCount { get; set; } = 3;
        public TimeSpan InitialDelay { get; set; } = TimeSpan.FromMilliseconds(100);
        public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(5);
        public bool UseExponentialBackoff { get; set; } = true;
        public double BackoffMultiplier { get; set; } = 2.0;

        public static RetryPolicy Default { get; } = new RetryPolicy
        {
            MaxRetryCount = 3,
            InitialDelay = TimeSpan.FromMilliseconds(100),
            MaxDelay = TimeSpan.FromSeconds(5),
            UseExponentialBackoff = true,
            BackoffMultiplier = 2.0
        };

        public static RetryPolicy Aggressive { get; } = new RetryPolicy
        {
            MaxRetryCount = 5,
            InitialDelay = TimeSpan.FromMilliseconds(50),
            MaxDelay = TimeSpan.FromSeconds(2),
            UseExponentialBackoff = true,
            BackoffMultiplier = 1.5
        };

        public static RetryPolicy Conservative { get; } = new RetryPolicy
        {
            MaxRetryCount = 2,
            InitialDelay = TimeSpan.FromMilliseconds(500),
            MaxDelay = TimeSpan.FromSeconds(10),
            UseExponentialBackoff = true,
            BackoffMultiplier = 2.0
        };
    }

    /// <summary>
    /// 熔断器状态
    /// </summary>
    public enum CircuitState
    {
        Closed,
        Open,
        HalfOpen
    }

    /// <summary>
    /// 熔断器异常事件参数
    /// </summary>
    public class CircuitBreakerException : Exception
    {
        public CircuitState LastState { get; }

        public CircuitBreakerException(string message, CircuitState lastState) : base(message)
        {
            LastState = lastState;
        }
    }

    /// <summary>
    /// 熔断器模式实现，防止级联故障
    /// </summary>
    public sealed class CircuitBreaker : IDisposable
    {
        private readonly object _lock = new object();
        private readonly int _failureThreshold;
        private readonly TimeSpan _openDuration;
        private readonly int _successThreshold;

        private CircuitState _state = CircuitState.Closed;
        private int _failureCount;
        private int _successCount;
        private DateTime _lastFailureTime = DateTime.MinValue;
        private DateTime _openedAt = DateTime.MinValue;

        public event EventHandler<CircuitState> StateChanged;

        public CircuitState State
        {
            get
            {
                lock (_lock)
                {
                    if (_state == CircuitState.Open && DateTime.Now - _openedAt >= _openDuration)
                    {
                        _state = CircuitState.HalfOpen;
                        OnStateChanged(_state);
                    }
                    return _state;
                }
            }
        }

        public CircuitBreaker(int failureThreshold = 5, TimeSpan? openDuration = null, int successThreshold = 3)
        {
            _failureThreshold = failureThreshold;
            _openDuration = openDuration ?? TimeSpan.FromSeconds(30);
            _successThreshold = successThreshold;
        }

        public TResult Execute<TResult>(Func<TResult> action)
        {
            ThrowIfOpen();
            try
            {
                TResult result = action();
                RecordSuccess();
                return result;
            }
            catch (Exception ex)
            {
                RecordFailure(ex);
                throw;
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
        {
            ThrowIfOpen();
            try
            {
                TResult result = await action().ConfigureAwait(false);
                RecordSuccess();
                return result;
            }
            catch (Exception ex)
            {
                RecordFailure(ex);
                throw;
            }
        }

        public void Execute(Action action)
        {
            ThrowIfOpen();
            try
            {
                action();
                RecordSuccess();
            }
            catch (Exception ex)
            {
                RecordFailure(ex);
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            ThrowIfOpen();
            try
            {
                await action().ConfigureAwait(false);
                RecordSuccess();
            }
            catch (Exception ex)
            {
                RecordFailure(ex);
                throw;
            }
        }

        private void ThrowIfOpen()
        {
            if (State == CircuitState.Open)
            {
                throw new CircuitBreakerException(
                    $"熔断器已打开，请在 {_openDuration.TotalSeconds} 秒后重试",
                    CircuitState.Open);
            }
        }

        private void RecordSuccess()
        {
            lock (_lock)
            {
                if (_state == CircuitState.HalfOpen)
                {
                    _successCount++;
                    if (_successCount >= _successThreshold)
                    {
                        _state = CircuitState.Closed;
                        _failureCount = 0;
                        _successCount = 0;
                        OnStateChanged(_state);
                    }
                }
                else
                {
                    _failureCount = 0;
                }
            }
        }

        private void RecordFailure(Exception ex)
        {
            lock (_lock)
            {
                _failureCount++;
                _lastFailureTime = DateTime.Now;

                if (_state == CircuitState.HalfOpen)
                {
                    _state = CircuitState.Open;
                    _openedAt = DateTime.Now;
                    OnStateChanged(_state);
                }
                else if (_failureCount >= _failureThreshold)
                {
                    _state = CircuitState.Open;
                    _openedAt = DateTime.Now;
                    OnStateChanged(_state);
                }
            }
        }

        private void OnStateChanged(CircuitState newState)
        {
            StateChanged?.Invoke(this, newState);
        }

        public void Reset()
        {
            lock (_lock)
            {
                _state = CircuitState.Closed;
                _failureCount = 0;
                _successCount = 0;
                _openedAt = DateTime.MinValue;
                OnStateChanged(_state);
            }
        }

        public void Dispose()
        {
        }
    }

    /// <summary>
    /// 重试辅助类，提供便捷的重试执行方法
    /// </summary>
    public static class RetryHelper
    {
        /// <summary>
        /// 使用指定策略执行操作，自动重试失败的调用
        /// </summary>
        public static TResult ExecuteWithRetry<TResult>(Func<TResult> operation, RetryPolicy policy = null, Action<Exception, int> onRetry = null)
        {
            policy = policy ?? RetryPolicy.Default;
            Exception lastException = null;
            int attemptCount = 0;

            while (attemptCount <= policy.MaxRetryCount)
            {
                try
                {
                    return operation();
                }
                catch (Exception ex) when (IsTransientException(ex))
                {
                    lastException = ex;
                    attemptCount++;

                    if (attemptCount > policy.MaxRetryCount)
                        break;

                    TimeSpan delay = CalculateDelay(policy, attemptCount);
                    onRetry?.Invoke(ex, attemptCount);
                    Thread.Sleep(delay);
                }
            }

            throw new AggregateException($"重试 {policy.MaxRetryCount} 次后仍然失败", lastException);
        }

        /// <summary>
        /// 异步执行带重试的操作
        /// </summary>
        public static async Task<TResult> ExecuteWithRetryAsync<TResult>(
            Func<Task<TResult>> operation,
            RetryPolicy policy = null,
            Action<Exception, int> onRetry = null,
            CancellationToken cancellationToken = default)
        {
            policy = policy ?? RetryPolicy.Default;
            Exception lastException = null;
            int attemptCount = 0;

            while (attemptCount <= policy.MaxRetryCount)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    return await operation().ConfigureAwait(false);
                }
                catch (Exception ex) when (IsTransientException(ex))
                {
                    lastException = ex;
                    attemptCount++;

                    if (attemptCount > policy.MaxRetryCount)
                        break;

                    TimeSpan delay = CalculateDelay(policy, attemptCount);
                    onRetry?.Invoke(ex, attemptCount);
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }

            throw new AggregateException($"重试 {policy.MaxRetryCount} 次后仍然失败", lastException);
        }

        /// <summary>
        /// 使用熔断器执行操作
        /// </summary>
        public static TResult ExecuteWithCircuitBreaker<TResult>(
            Func<TResult> operation,
            CircuitBreaker circuitBreaker)
        {
            return circuitBreaker.Execute(operation);
        }

        /// <summary>
        /// 异步执行带熔断器的操作
        /// </summary>
        public static async Task<TResult> ExecuteWithCircuitBreakerAsync<TResult>(
            Func<Task<TResult>> operation,
            CircuitBreaker circuitBreaker)
        {
            return await circuitBreaker.ExecuteAsync(operation).ConfigureAwait(false);
        }

        private static bool IsTransientException(Exception ex)
        {
            if (ex is TimeoutException) return true;
            if (ex is System.IO.IOException) return true;
            if (ex is System.Net.Sockets.SocketException) return true;
            if (ex is System.IO.IOException) return true;
            if (ex is OperationCanceledException) return false;
            return ex is TimeoutException;
        }

        private static TimeSpan CalculateDelay(RetryPolicy policy, int attemptCount)
        {
            if (!policy.UseExponentialBackoff)
                return policy.InitialDelay;

            double multiplier = Math.Pow(policy.BackoffMultiplier, attemptCount - 1);
            long ticks = (long)(policy.InitialDelay.Ticks * multiplier);
            return TimeSpan.FromTicks(Math.Min(ticks, policy.MaxDelay.Ticks));
        }
    }
}