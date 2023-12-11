namespace UmbrellaOS.Generic.Extensions
{
    public static class ByteExtensions
    {
        /** <summary>Fill the array with 0.</summary> */
        public static void FillWithZero(this byte[] buffer) => buffer.FillWith(0);
        /** <summary>Fill the array with the value.</summary> */
        public static void FillWith(this byte[] buffer, byte value)
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = value;
        }
        /**
         * <summary>Retrieve one bit from the specified position from right to left in a byte.</summary>
         * <returns>true if the value is 1; false if the value is 0</returns>
         */
        public static bool GetBitLittleEndian(this byte b, int digit) => (b & (0x01 << digit)) > 0;
        /**
         * <summary>
         * Set the bit value at the specified position from right to left in a byte.<br/>
         * if the given value is true, the bit will be set to 1;<br/>
         * if the given value is false, the bit will be set to 0.
         * </summary>
         */
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