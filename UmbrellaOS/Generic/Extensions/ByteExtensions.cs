namespace UmbrellaOS.Generic.Extensions
{
    public static class ByteExtensions
    {
        public static void FillWithZero(this byte[] buffer) => buffer.FillWith(0);
        public static void FillWith(this byte[] buffer, byte value)
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = value;
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