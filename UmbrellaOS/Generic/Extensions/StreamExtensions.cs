namespace UmbrellaOS.Generic.Extensions
{
    public static class StreamExtensions
    {
        /**
         * <summary>
         * Asynchronously writes a byte to the current stream and advances the current position within this stream by 1.
         * </summary>
         * <param name="stream">the binary stream to write</param>
         * <param name="value">the byte value to write to the stream</param>
         * <param name="cancellationToken">the token indicating whether the task has been cancelled</param>
         */
        public static async Task WriteByteAsync(this Stream stream, byte value, CancellationToken cancellationToken = default)
        {
            await stream.WriteAsync(new byte[1] { value }, cancellationToken);
        }
    }
}