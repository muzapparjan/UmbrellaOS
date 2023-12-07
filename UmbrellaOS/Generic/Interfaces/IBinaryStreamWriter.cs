namespace UmbrellaOS.Generic.Interfaces
{
    public interface IBinaryStreamWriter
    {
        public void Write(Stream stream);
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}