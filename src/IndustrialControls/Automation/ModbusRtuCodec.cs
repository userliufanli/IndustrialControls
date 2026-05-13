using System;

namespace IndustrialControls.Automation
{
    public static class ModbusRtuCodec
    {
        public static ushort ComputeCrc(byte[] data, int offset, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < length; i++)
            {
                crc ^= data[offset + i];
                for (int b = 0; b < 8; b++)
                {
                    if ((crc & 1) != 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc >>= 1;
                }
            }
            return crc;
        }

        public static void AppendCrc(byte[] frameWithoutCrc, int length)
        {
            if (frameWithoutCrc == null) throw new ArgumentNullException(nameof(frameWithoutCrc));
            if (length + 2 > frameWithoutCrc.Length)
                throw new ArgumentException("缓冲区不足以容纳 CRC。");
            ushort crc = ComputeCrc(frameWithoutCrc, 0, length);
            frameWithoutCrc[length] = (byte)(crc & 0xFF);
            frameWithoutCrc[length + 1] = (byte)(crc >> 8);
        }

        public static bool VerifyCrc(byte[] frame, int length)
        {
            if (frame == null || length < 3) return false;
            ushort got = (ushort)(frame[length - 2] | (frame[length - 1] << 8));
            ushort calc = ComputeCrc(frame, 0, length - 2);
            return got == calc;
        }

        public static byte[] BuildReadHoldingRegistersRequest(byte unitId, ushort startAddress, ushort quantity)
        {
            if (quantity == 0 || quantity > 125)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            var buf = new byte[8];
            buf[0] = unitId;
            buf[1] = 0x03;
            buf[2] = (byte)(startAddress >> 8);
            buf[3] = (byte)(startAddress & 0xFF);
            buf[4] = (byte)(quantity >> 8);
            buf[5] = (byte)(quantity & 0xFF);
            AppendCrc(buf, 6);
            return buf;
        }

        public static byte[] BuildWriteSingleRegisterRequest(byte unitId, ushort address, ushort value)
        {
            var buf = new byte[8];
            buf[0] = unitId;
            buf[1] = 0x06;
            buf[2] = (byte)(address >> 8);
            buf[3] = (byte)(address & 0xFF);
            buf[4] = (byte)(value >> 8);
            buf[5] = (byte)(value & 0xFF);
            AppendCrc(buf, 6);
            return buf;
        }
    }
}
