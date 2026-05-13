using System;
using System.Windows.Forms;

namespace IndustrialControls.Utilities
{
    /// <summary>
    /// 动画辅助类，基于Timer的动画引擎
    /// </summary>
    public class AnimationHelper : IDisposable
    {
        private Timer _timer;
        private bool _blinkState;
        private float _animationProgress;
        private bool _disposed;

        /// <summary>
        /// 闪烁状态变更事件
        /// </summary>
        public event EventHandler<bool> BlinkStateChanged;

        /// <summary>
        /// 动画进度变更事件
        /// </summary>
        public event EventHandler<float> ProgressChanged;

        /// <summary>
        /// 闪烁间隔（毫秒）
        /// </summary>
        public int BlinkInterval
        {
            get => _timer?.Interval ?? 500;
            set
            {
                if (_timer != null)
                    _timer.Interval = Math.Max(50, value);
            }
        }

        /// <summary>
        /// 是否正在闪烁
        /// </summary>
        public bool IsBlinking => _timer?.Enabled ?? false;

        /// <summary>
        /// 当前闪烁状态
        /// </summary>
        public bool BlinkState => _blinkState;

        /// <summary>
        /// 动画进度 (0.0 ~ 1.0)
        /// </summary>
        public float AnimationProgress => _animationProgress;

        public AnimationHelper()
        {
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Tick += OnTimerTick;
        }

        /// <summary>
        /// 开始闪烁
        /// </summary>
        public void StartBlink(int intervalMs = 500)
        {
            _timer.Interval = Math.Max(50, intervalMs);
            _blinkState = true;
            _timer.Start();
        }

        /// <summary>
        /// 停止闪烁
        /// </summary>
        public void StopBlink()
        {
            _timer.Stop();
            _blinkState = true;
            BlinkStateChanged?.Invoke(this, _blinkState);
        }

        /// <summary>
        /// 开始渐变动画
        /// </summary>
        public void StartFadeAnimation(int durationMs = 300)
        {
            _animationProgress = 0f;
            _timer.Interval = 16; // ~60fps
            _timer.Start();
        }

        /// <summary>
        /// 重置动画
        /// </summary>
        public void Reset()
        {
            _timer.Stop();
            _blinkState = true;
            _animationProgress = 0f;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _blinkState = !_blinkState;
            BlinkStateChanged?.Invoke(this, _blinkState);

            _animationProgress += 0.05f;
            if (_animationProgress > 1f)
                _animationProgress = 0f;
            ProgressChanged?.Invoke(this, _animationProgress);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _timer?.Stop();
                _timer?.Dispose();
                _timer = null;
                _disposed = true;
            }
        }
    }
}
