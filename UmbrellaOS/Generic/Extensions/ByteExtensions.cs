namespace UmbrellaOS.Generic.Extensions;

public static class ByteExtensions
{
    /**
     * <summary>Retrieve one bit from the specified position from right to left in a byte.</summary>
     * <param name="b">the byte to get bit from</param>
     * <param name="digit">the position of the bit to get</param>
     * <returns>0 or 1</returns>
     * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static Bit GetBitLittleEndian(this byte b, int digit)
    {
        if (digit < 0 || digit > 7)
            throw new ArgumentOutOfRangeException(nameof(digit), "digit should be from 0 to 7");
        return (b & (0x01 << digit)) > 0;
    }
    /**
     * <summary>Retrieve one bit from the specified position from left to right in a byte.</summary>
     * <param name="b">the byte to get bit from</param>
     * <param name="digit">the position of the bit to get</param>
     * <returns>0 or 1</returns>
     * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static Bit GetBitBigEndian(this byte b, int digit) => b.GetBitLittleEndian(7 - digit);

    /**
     * <summary>
     * Set the bit value at the specified position from right to left in a byte.
     * </summary>
     * <param name="b">the byte to set the bit to</param>
     * <param name="digit">the position of the bit to set</param>
     * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static void SetBitLittleEndian(this ref byte b, int digit, Bit value)
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
     * <summary>
     * Set the bit value at the specified position from left to right in a byte.
     * </summary>
     * <param name="b">the byte to set the bit to</param>
     * <param name="digit">the position of the bit to set</param>
     * <exception cref="ArgumentOutOfRangeException">If digit is not within the range of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static void SetBitBigEndian(this ref byte b, int digit, Bit value) => b.SetBitLittleEndian(7 - digit, value);

    /**
     * <summary>Retrieve a bit sequence from the specified position from right to left in a byte.</summary>
     * <param name="b">the byte to get bit from</param>
     * <param name="start">the position of the starting bit to get</param>
     * <param name="count">the bit count to get totally</param>
     * <returns>the bit sequence to get from the byte</returns>
     * <exception cref="ArgumentOutOfRangeException">If the bit range is out of range, an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static Bit[] GetBitsLittleEndian(this byte b, int start, int count)
    {
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), $"invalid bit index {start}");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), $"invalid bit count {count}");
        if (start + count > 8)
            throw new ArgumentOutOfRangeException(nameof(count), $"invalid range [{start}, {start + count - 1}]");
        var bits = new Bit[count];
        for (var i = 0; i < count; i++)
            bits[i] = b.GetBitLittleEndian(start + i);
        return bits;
    }
    /**
     * <summary>Retrieve a bit sequence from the specified position from left to right in a byte.</summary>
     * <param name="b">the byte to get bit from</param>
     * <param name="start">the position of the starting bit to get</param>
     * <param name="count">the bit count to get totally</param>
     * <returns>the bit sequence to get from the byte</returns>
     * <exception cref="ArgumentOutOfRangeException">If the bit range is out of range, an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static Bit[] GetBitsBigEndian(this byte b, int start, int count)
    {
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), $"invalid bit index {start}");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), $"invalid bit count {count}");
        if (start + count > 8)
            throw new ArgumentOutOfRangeException(nameof(count), $"invalid range [{start}, {start + count - 1}]");
        var bits = new Bit[count];
        for (var i = 0; i < count; i++)
            bits[i] = b.GetBitBigEndian(start + i);
        return bits;
    }

    /**
     * <summary>
     * Set some bit values from right to left in a byte.
     * </summary>
     * <param name="b">the byte to set bits to</param>
     * <param name="start">the starting position of the bit to set from</param>
     * <param name="bits">the bit sequence to set to the byte</param>
     * <exception cref="ArgumentOutOfRangeException">If the target range is not a subset of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static void SetBitsLittleEndian(this ref byte b, int start, Bit[] bits)
    {
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), $"invalid bit index {start}");
        if (start + bits.Length > 8)
            throw new ArgumentOutOfRangeException(nameof(bits), $"invalid range [{start}, {start + bits.Length - 1}]");
        for (var i = 0; i < bits.Length; i++)
            b.SetBitLittleEndian(start + i, bits[i]);
    }
    /**
     * <summary>
     * Set some bit values from left to right in a byte.
     * </summary>
     * <param name="b">the byte to set bits to</param>
     * <param name="start">the starting position of the bit to set from</param>
     * <param name="bits">the bit sequence to set to the byte</param>
     * <exception cref="ArgumentOutOfRangeException">If the target range is not a subset of [0, 7], an ArgumentOutOfRange exception may be thrown.</exception>
     */
    public static void SetBitsBigEndian(this ref byte b, int start, Bit[] bits)
    {
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start), $"invalid bit index {start}");
        if (start + bits.Length > 8)
            throw new ArgumentOutOfRangeException(nameof(bits), $"invalid range [{start}, {start + bits.Length - 1}]");
        for (var i = 0; i < bits.Length; i++)
            b.SetBitBigEndian(start + i, bits[i]);
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