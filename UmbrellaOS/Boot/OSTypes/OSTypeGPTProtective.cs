using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.OSTypes
{
    public sealed class OSTypeGPTProtective : IOSType
    {
        public static readonly OSTypeGPTProtective Default = new();

        public byte Value => 0xEE;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteAsync(new byte[1] { Value }, cancellationToken);
        }
    }
}