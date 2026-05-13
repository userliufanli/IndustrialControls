using System;
using System.Collections.Concurrent;
using System.Threading;

namespace IndustrialControls.Utilities
{
    /// <summary>
    /// 高性能字节缓冲区池，减少 GC 压力
    /// </summary>
    public sealed class ByteBufferPool
    {
        private static readonly Lazy<ByteBufferPool> _instance = new Lazy<ByteBufferPool>(() => new ByteBufferPool(), LazyThreadSafetyMode.PublicationOnly);
        public static ByteBufferPool Instance => _instance.Value;

        private readonly ConcurrentDictionary<int, ConcurrentBag<PooledBuffer>> _pools;
        private readonly int _maxBufferSize;
        private readonly int _maxBuffersPerSize;

        private ByteBufferPool()
        {
            _pools = new ConcurrentDictionary<int, ConcurrentBag<PooledBuffer>>();
            _maxBufferSize = 65536;
            _maxBuffersPerSize = 16;

            _pools.TryAdd(256, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(512, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(1024, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(2048, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(4096, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(8192, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(16384, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(32768, new ConcurrentBag<PooledBuffer>());
            _pools.TryAdd(65536, new ConcurrentBag<PooledBuffer>());
        }

        public static PooledBuffer Rent(int minimumLength)
        {
            return Instance.RentInternal(minimumLength);
        }

        public static void Return(PooledBuffer buffer)
        {
            if (buffer != null && buffer.Array != null)
            {
                Instance.ReturnInternal(buffer);
            }
        }

        private PooledBuffer RentInternal(int minimumLength)
        {
            int bufferSize = GetNearestSize(minimumLength);
            bufferSize = Math.Max(256, Math.Min(bufferSize, _maxBufferSize));

            if (_pools.TryGetValue(bufferSize, out var bag))
            {
                if (bag.TryTake(out var pooledBuffer))
                {
                    pooledBuffer.PrepareForRentFromPool();
                    return pooledBuffer;
                }
            }

            byte[] array = new byte[bufferSize];
            return new PooledBuffer(array, bufferSize, this);
        }

        private void ReturnInternal(PooledBuffer buffer)
        {
            if (buffer.Array != null && buffer.Count < buffer.Capacity)
            {
                Array.Clear(buffer.Array, 0, buffer.Count);
            }

            if (_pools.TryGetValue(buffer.Capacity, out var bag) && bag.Count < _maxBuffersPerSize)
            {
                bag.Add(buffer);
            }
        }

        private static int GetNearestSize(int value)
        {
            if (value <= 256) return 256;
            if (value <= 512) return 512;
            if (value <= 1024) return 1024;
            if (value <= 2048) return 2048;
            if (value <= 4096) return 4096;
            if (value <= 8192) return 8192;
            if (value <= 16384) return 16384;
            if (value <= 32768) return 32768;
            return 65536;
        }

        public sealed class PooledBuffer : IDisposable
        {
            private byte[] _array;
            private int _count;
            private int _capacity;
            private readonly ByteBufferPool _owner;
            private bool _disposed;

            internal PooledBuffer(byte[] array, int capacity, ByteBufferPool owner)
            {
                _array = array;
                _capacity = capacity;
                _owner = owner;
                _count = 0;
            }

            public byte[] Array => _array;

            public int Count
            {
                get => _count;
                set
                {
                    if (value < 0 || value > _capacity)
                        throw new ArgumentOutOfRangeException(nameof(value));
                    _count = value;
                }
            }

            public int Capacity => _capacity;

            public void Advance(int count)
            {
                if (count < 0 || _count + count > _capacity)
                    throw new ArgumentOutOfRangeException(nameof(count));
                _count += count;
            }

            public void Reset()
            {
                _count = 0;
                if (_array != null && _capacity > 0)
                {
                    for (int i = 0; i < _capacity; i++)
                        _array[i] = 0;
                }
            }

            public byte[] ToArray()
            {
                if (_count == 0) return new byte[0];
                if (_array == null)
                    return new byte[0];
                byte[] result = new byte[_count];
                Buffer.BlockCopy(_array, 0, result, 0, _count);
                return result;
            }

            /// <summary>
            /// 从池中取出再次租用时：清除已释放标记、保证 backing array 有效并清零（旧版 Dispose 曾将 <see cref="_array"/> 置 null 导致 ReadAsync buffer 为 null）。
            /// </summary>
            internal void PrepareForRentFromPool()
            {
                _disposed = false;
                if (_array == null || _array.Length != _capacity)
                    _array = new byte[_capacity];
                _count = 0;
                for (int i = 0; i < _capacity; i++)
                    _array[i] = 0;
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _disposed = true;
                    // 仅归还到池中复用，不得清空 _array，否则下次 Rent 后 Array 为 null 会触发 NetworkStream.ReadAsync 等 API 的 ArgumentNullException(buffer)
                    _owner?.ReturnInternal(this);
                }
            }
        }
    }
}