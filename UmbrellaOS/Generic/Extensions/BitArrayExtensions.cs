namespace UmbrellaOS.Generic.Extensions;

public static class BitArrayExtensions
{
    /**
     * <summary>Convert bit array to a byte in little endian order.</summary>
     * <param name="bits">the bit sequence to convert</param>
     * <exception cref="ArgumentException">An ArgumentException may be thrown if the bit count is not 8</exception>
     */
    public static byte AsByteLittleEndian(this Bit[] bits)
    {
        if (bits.Length != 8)
            throw new ArgumentException($"the bit count should be 8", nameof(bits));
        byte result = 0x00;
        for (var i = 0; i < 8; i++)
            result |= (byte)(bits[i] << i);
        return result;
    }
    /**
     * <summary>Convert bit array to a byte in big endian order.</summary>
     * <param name="bits">the bit sequence to convert</param>
     * <exception cref="ArgumentException">An ArgumentException may be thrown if the bit count is not 8</exception>
     */
    public static byte AsByteBigEndian(this Bit[] bits)
    {
        if (bits.Length != 8)
            throw new ArgumentException($"the bit count should be 8", nameof(bits));
        byte result = 0x00;
        for (var i = 0; i < 8; i++)
            result |= (byte)(bits[i] << (7 - i));
        return result;
    }
}