namespace UmbrellaOS.Generic.Interfaces
{
    /** <summary>Indicates the ability to write bytes to a stream synchronously and/or asynchronously.</summary> */
    public interface IBinaryStreamWriter
    {
        /** <summary>Write bytes to the stream synchronously</summary> */
        public void Write(Stream stream);
        /** <summary>Write bytes to the stream asynchronously</summary> */
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}