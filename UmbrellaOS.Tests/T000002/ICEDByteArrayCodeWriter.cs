using Iced.Intel;

namespace UmbrellaOS.Tests.T000002;

internal sealed class ICEDByteArrayCodeWriter : CodeWriter
{
    public byte[] Value => [.. bytes];

    private readonly List<byte> bytes = [];

    public override void WriteByte(byte value)
    {
        bytes.Add(value);
    }
    public void Clear() => bytes.Clear();
}