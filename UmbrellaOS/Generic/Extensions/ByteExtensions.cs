namespace UmbrellaOS.Generic.Extensions
{
    public static class ByteExtensions
    {
        /**
         * <summary>Fill the array with 0.</summary>
         * <param name="buffer">the array to fill with</param>
         * <exception cref="OverflowException">Overflow may occur while getting the buffer length.</exception>
         */
        public static void FillWithZero(this byte[] buffer) => buffer.FillWith(0);
        /**
         * <summary>Fill the array with the value.</summary>
         * <param name="buffer">the array to fill with</param>
         * <param name="value">the given byte value to fill with the entire array</param>
         * <exception cref="OverflowException">Overflow may occur while getting the buffer length.</exception>
         */
        public static void FillWith(this byte[] buffer, byte value)
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = value;
        }
        /**
         * <summary>Retrieve one bit from the specified position from right to left in a byte.</summary>
         * <param name="b">the byte to get bit from</param>
         * <param name="digit">the position of the bit to get</param>
         * <returns>true if the value is 1; false if the value is 0</returns>
         * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public static byte GetBitLittleEndian(this byte b, int digit)
        {
            if (digit < 0 || digit > 7)
                throw new ArgumentOutOfRangeException(nameof(digit), "digit should be from 0 to 7");
            return (b & (0x01 << digit)) > 0 ? (byte)1 : (byte)0;
        }
        /**
         * <summary>
         * Set the bit value at the specified position from right to left in a byte.<br/>
         * if the given value is true, the bit will be set to 1;<br/>
         * if the given value is false, the bit will be set to 0.
         * </summary>
         * <param name="b">the byte to set the bit to</param>
         * <param name="digit">the position of the bit to set</param>
         * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public static void SetBitLittleEndian(this ref byte b, int digit, byte value)
        {
            if (digit < 0 || digit > 7)
                throw new ArgumentOutOfRangeException(nameof(digit), "digit should be from 0 to 7");
            var mask = (byte)(0x01 << digit);
            if (value == 1)
                b |= mask;
            else
                b &= (byte)(~mask);
        }
        /**
         * <summary>Copy the bit value at the specified position from src to dst.</summary>
         * <param name="src">the byte to get bit from</param>
         * <param name="dst">the byte to set bit to</param>
         * <param name="srcDigit">the position of the bit to get from src</param>
         * <param name="dstDigit">the position of the bit to set to dst</param>
         * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public static void CopyBitLittleEndianTo(this byte src, ref byte dst, int srcDigit, int dstDigit)
        {
            var bit = src.GetBitLittleEndian(srcDigit);
            dst.SetBitLittleEndian(dstDigit, bit);
        }
        /**
         * <summary>Copy bit values in the specified range from src to dst.</summary>
         * <param name="src">the byte to get bits from</param>
         * <param name="dst">the byte to set bits to</param>
         * <param name="srcDigitFrom">the start position of the range to get from src</param>
         * <param name="dstDigitFrom">the start position of the range to set to dst</param>
         * <param name="digitCount">the length of the range to copy</param>
         * <exception cref="ArgumentOutOfRangeException">
         * If digit is not within the range of [0, 7], 
         * or the digit count is not within the range of [1, 8],
         * an ArgumentOutOfRange exception may be thrown.
         * </exception>
         */
        public static void CopyBitsLittleEndianTo(this byte src, ref byte dst, int srcDigitFrom, int dstDigitFrom, int digitCount)
        {
            if (digitCount <= 0 || digitCount > 8)
                throw new ArgumentOutOfRangeException(nameof(digitCount), "digit count should be from 1 to 8");
            for (var offset = 0; offset < digitCount; offset++)
            {
                var srcDigit = srcDigitFrom + offset;
                var dstDigit = dstDigitFrom + offset;
                src.CopyBitLittleEndianTo(ref dst, srcDigit, dstDigit);
            }
        }
    }
}