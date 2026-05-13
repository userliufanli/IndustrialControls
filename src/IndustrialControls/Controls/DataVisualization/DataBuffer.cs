using System;
using System.Threading;

namespace IndustrialControls.Controls.DataVisualization
{
    /// <summary>
    /// 高性能环形数据缓冲区，线程安全
    /// 优化：缓存 Min/Max 值，避免每次遍历计算
    /// </summary>
    public class DataBuffer
    {
        private readonly double[] _buffer;
        private int _head;
        private int _count;
        private readonly object _syncLock = new object();
        
        // 缓存的统计值，避免每次遍历计算
        private double _cachedMin = double.MaxValue;
        private double _cachedMax = double.MinValue;
        private bool _statsValid = false;

        /// <summary>
        /// 缓冲区容量
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// 当前数据点数量
        /// </summary>
        public int Count
        {
            get { lock (_syncLock) return _count; }
        }

        /// <summary>
        /// 是否已满
        /// </summary>
        public bool IsFull
        {
            get { lock (_syncLock) return _count >= Capacity; }
        }

        /// <summary>
        /// 最新值
        /// </summary>
        public double Latest
        {
            get
            {
                lock (_syncLock)
                {
                    if (_count == 0) return 0;
                    int index = (_head - 1 + Capacity) % Capacity;
                    return _buffer[index];
                }
            }
        }

        /// <summary>
        /// 最小值（缓存优化）
        /// </summary>
        public double Min
        {
            get
            {
                lock (_syncLock)
                {
                    if (_count == 0) return 0;
                    if (!_statsValid)
                    {
                        RecalculateStats();
                    }
                    return _cachedMin;
                }
            }
        }

        /// <summary>
        /// 最大值（缓存优化）
        /// </summary>
        public double Max
        {
            get
            {
                lock (_syncLock)
                {
                    if (_count == 0) return 0;
                    if (!_statsValid)
                    {
                        RecalculateStats();
                    }
                    return _cachedMax;
                }
            }
        }

        public DataBuffer(int capacity = 1000)
        {
            Capacity = Math.Max(10, capacity);
            _buffer = new double[Capacity];
        }

        /// <summary>
        /// 重新计算统计值（最小值和最大值）
        /// </summary>
        private void RecalculateStats()
        {
            if (_count == 0)
            {
                _cachedMin = double.MaxValue;
                _cachedMax = double.MinValue;
                _statsValid = true;
                return;
            }

            double min = double.MaxValue;
            double max = double.MinValue;

            for (int i = 0; i < _count; i++)
            {
                int idx = ((_head - _count + i) + Capacity) % Capacity;
                double val = _buffer[idx];
                if (val < min) min = val;
                if (val > max) max = val;
            }

            _cachedMin = min;
            _cachedMax = max;
            _statsValid = true;
        }

        /// <summary>
        /// 添加数据点（优化：更新缓存的统计值）
        /// </summary>
        public void Add(double value)
        {
            lock (_syncLock)
            {
                // 如果缓冲区已满且即将覆盖的值是当前的 Min 或 Max，需要标记统计失效
                if (_count >= Capacity)
                {
                    int oldestIndex = _head; // 即将被覆盖的位置
                    double oldestValue = _buffer[oldestIndex];
                    if (_statsValid && (oldestValue == _cachedMin || oldestValue == _cachedMax))
                    {
                        _statsValid = false;
                    }
                }

                _buffer[_head] = value;
                _head = (_head + 1) % Capacity;
                
                if (_count < Capacity)
                {
                    _count++;
                    // 新值可能影响 Min/Max
                    if (_statsValid)
                    {
                        if (value < _cachedMin) _cachedMin = value;
                        if (value > _cachedMax) _cachedMax = value;
                    }
                }
                else
                {
                    // 缓冲区已满，检查新值是否需要更新缓存
                    if (_statsValid)
                    {
                        if (value < _cachedMin) _cachedMin = value;
                        if (value > _cachedMax) _cachedMax = value;
                    }
                }
            }
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        public void AddRange(double[] values)
        {
            if (values == null || values.Length == 0) return;
            
            lock (_syncLock)
            {
                // 批量添加时直接标记统计失效，避免逐个更新的复杂性
                _statsValid = false;
                
                foreach (var v in values)
                {
                    _buffer[_head] = v;
                    _head = (_head + 1) % Capacity;
                    if (_count < Capacity) _count++;
                }
            }
        }

        /// <summary>
        /// 获取所有数据（从旧到新排列）
        /// </summary>
        public double[] ToArray()
        {
            lock (_syncLock)
            {
                var result = new double[_count];
                for (int i = 0; i < _count; i++)
                {
                    int idx = ((_head - _count + i) + Capacity) % Capacity;
                    result[i] = _buffer[idx];
                }
                return result;
            }
        }

        /// <summary>
        /// 获取最近N个数据点
        /// </summary>
        public double[] GetLatest(int count)
        {
            lock (_syncLock)
            {
                int actual = Math.Min(count, _count);
                var result = new double[actual];
                for (int i = 0; i < actual; i++)
                {
                    int idx = ((_head - actual + i) + Capacity) % Capacity;
                    result[i] = _buffer[idx];
                }
                return result;
            }
        }

        /// <summary>
        /// 获取平均值
        /// </summary>
        public double Average
        {
            get
            {
                lock (_syncLock)
                {
                    if (_count == 0) return 0;
                    
                    double sum = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        int idx = ((_head - _count + i) + Capacity) % Capacity;
                        sum += _buffer[idx];
                    }
                    return sum / _count;
                }
            }
        }

        /// <summary>
        /// 获取数据总和
        /// </summary>
        public double Sum
        {
            get
            {
                lock (_syncLock)
                {
                    if (_count == 0) return 0;
                    
                    double sum = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        int idx = ((_head - _count + i) + Capacity) % Capacity;
                        sum += _buffer[idx];
                    }
                    return sum;
                }
            }
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            lock (_syncLock)
            {
                _head = 0;
                _count = 0;
                _cachedMin = double.MaxValue;
                _cachedMax = double.MinValue;
                _statsValid = true;
            }
        }
    }
}
