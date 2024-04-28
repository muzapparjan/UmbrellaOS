namespace UmbrellaOS.Generic.Extensions;

public static class ByteArrayExtensions
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
    public static void FillWith(this byte[] buffer, Bit value)
    {
        for (var i = 0; i < buffer.Length; i++)
            buffer[i] = value;
    }
}