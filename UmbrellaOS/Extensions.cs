using System.Buffers.Binary;

namespace UmbrellaOS
{
    public static class Extensions
    {
        public static void FillWithZero(this byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 0;
        }
        public static byte[] GetBytesLittleEndian(this uint value)
        {
            var bytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
            return bytes;
        }
        public static byte[] GetBytesLittleEndian(this ulong value)
        {
            var bytes = new byte[8];
            BinaryPrimitives.WriteUInt64LittleEndian(bytes, value);
            return bytes;
        }
        public static bool GetBitLittleEndian(this byte b, int digit) => (b & (0x01 << digit)) > 0;
        public static void SetBitLittleEndian(this ref byte b, int digit, bool value)
        {
            if (digit < 0 || digit > 7)
                throw new ArgumentOutOfRangeException(nameof(digit), "digit should be from 0 to 7");
            var mask = (byte)(0x01 << digit);
            if (value)
                b |= mask;
            else
                b &= (byte)(~mask);
        }
    }
}