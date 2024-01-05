using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot.OSTypes
{
    public sealed class OSTypeUEFISystemPartition : IOSType
    {
        public static readonly OSTypeUEFISystemPartition Default = new();

        public byte Value => 0xEF;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(Value, cancellationToken);
        }
    }
}