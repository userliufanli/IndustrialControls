using System;
using System.Collections.Generic;

namespace IndustrialControls.Controls.Communication
{
    /// <summary>
    /// 分隔符帧解析器 - 管理接收缓冲区，按分隔符拆分数据帧
    /// 使用 KMP 算法优化分隔符查找，减少数组复制操作
    /// </summary>
    internal class FrameParser
    {
        private byte[] _buffer = new byte[0];
        private int _bufferLength = 0;
        private readonly object _lock = new object();
        private int[] _kmpTable;
        private byte[] _cachedDelimiter;

        /// <summary>帧分隔符（默认 \r\n）</summary>
        public byte[] Delimiter
        {
            get => _cachedDelimiter;
            set
            {
                _cachedDelimiter = value ?? new byte[] { 0x0D, 0x0A };
                BuildKmpTable();
            }
        }

        /// <summary>缓冲区最大容量（防止内存溢出，默认 64KB）</summary>
        public int MaxBufferSize { get; set; } = 65536;

        public FrameParser()
        {
            Delimiter = new byte[] { 0x0D, 0x0A };
        }

        /// <summary>
        /// 构建 KMP 算法的部分匹配表
        /// </summary>
        private void BuildKmpTable()
        {
            if (_cachedDelimiter == null || _cachedDelimiter.Length == 0)
            {
                _kmpTable = new int[0];
                return;
            }

            int[] table = new int[_cachedDelimiter.Length];
            int len = 0;
            int i = 1;

            while (i < _cachedDelimiter.Length)
            {
                if (_cachedDelimiter[i] == _cachedDelimiter[len])
                {
                    len++;
                    table[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = table[len - 1];
                    }
                    else
                    {
                        table[i] = 0;
                        i++;
                    }
                }
            }

            _kmpTable = table;
        }

        /// <summary>
        /// 追加数据到缓冲区，返回解析出的完整帧列表
        /// </summary>
        /// <param name="data">新接收的数据</param>
        /// <returns>解析出的完整帧列表（不含分隔符）。无完整帧时返回空列表。</returns>
        public List<byte[]> Append(byte[] data)
        {
            if (data == null || data.Length == 0)
                return new List<byte[]>();

            lock (_lock)
            {
                // 预分配缓冲区，减少内存分配次数
                EnsureBufferCapacity(_bufferLength + data.Length);

                // 将新数据复制到缓冲区末尾
                Buffer.BlockCopy(data, 0, _buffer, _bufferLength, data.Length);
                _bufferLength += data.Length;

                // 缓冲区溢出保护：丢弃最前面的数据
                if (_bufferLength > MaxBufferSize)
                {
                    int excess = _bufferLength - MaxBufferSize;
                    Buffer.BlockCopy(_buffer, excess, _buffer, 0, MaxBufferSize);
                    _bufferLength = MaxBufferSize;
                }

                // 使用 KMP 算法查找分隔符，提取完整帧
                var frames = new List<byte[]>();
                int searchStart = 0;

                while (true)
                {
                    int delimIndex = KmpSearch(_buffer, _bufferLength, searchStart);
                    if (delimIndex < 0) break;

                    // 提取帧数据（不含分隔符）
                    int frameLength = delimIndex - searchStart;
                    if (frameLength > 0)
                    {
                        byte[] frame = new byte[frameLength];
                        Buffer.BlockCopy(_buffer, searchStart, frame, 0, frameLength);
                        frames.Add(frame);
                    }

                    searchStart = delimIndex + _cachedDelimiter.Length;
                }

                // 移除已处理的数据，保留未完成帧
                if (searchStart > 0 && searchStart < _bufferLength)
                {
                    int remaining = _bufferLength - searchStart;
                    Buffer.BlockCopy(_buffer, searchStart, _buffer, 0, remaining);
                    _bufferLength = remaining;
                }
                else
                {
                    _bufferLength = 0;
                }

                return frames;
            }
        }

        /// <summary>
        /// 确保缓冲区有足够容量
        /// </summary>
        private void EnsureBufferCapacity(int requiredSize)
        {
            if (_buffer == null || _buffer.Length < requiredSize)
            {
                // 按 2 的幂次增长，减少内存分配次数
                int newCapacity = Math.Max(4096, _buffer?.Length ?? 0);
                while (newCapacity < requiredSize)
                {
                    newCapacity *= 2;
                }
                // 限制最大容量
                newCapacity = Math.Min(newCapacity, MaxBufferSize * 2);

                byte[] newBuffer = new byte[newCapacity];
                if (_buffer != null && _bufferLength > 0)
                {
                    Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _bufferLength);
                }
                _buffer = newBuffer;
            }
        }

        /// <summary>
        /// 使用 KMP 算法在 buffer 中查找分隔符
        /// </summary>
        private int KmpSearch(byte[] buffer, int bufferLength, int startIndex)
        {
            if (_cachedDelimiter == null || _cachedDelimiter.Length == 0)
                return -1;

            int delimiterLength = _cachedDelimiter.Length;
            if (bufferLength - startIndex < delimiterLength)
                return -1;

            int i = startIndex; // 主串索引
            int j = 0; // 模式串索引

            while (i < bufferLength)
            {
                if (_cachedDelimiter[j] == buffer[i])
                {
                    i++;
                    j++;
                }

                if (j == delimiterLength)
                {
                    // 找到匹配，返回起始位置
                    return i - j;
                }
                else if (i < bufferLength && _cachedDelimiter[j] != buffer[i])
                {
                    if (j != 0)
                    {
                        j = _kmpTable[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Reset()
        {
            lock (_lock)
            {
                _bufferLength = 0;
            }
        }
    }
}
