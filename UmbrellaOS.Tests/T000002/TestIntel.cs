using Iced.Intel;
using UmbrellaOS.Instruction.Encoding;

namespace UmbrellaOS.Tests.T000002;

internal static class TestIntel
{
    private static readonly ICEDByteArrayCodeWriter codeWriter16 = new();
    private static readonly ICEDByteArrayCodeWriter codeWriter32 = new();
    private static readonly ICEDByteArrayCodeWriter codeWriter64 = new();
    private static readonly Encoder encoder16 = Encoder.Create(16, codeWriter16);
    private static readonly Encoder encoder32 = Encoder.Create(32, codeWriter32);
    private static readonly Encoder encoder64 = Encoder.Create(64, codeWriter64);

    internal static async Task TestAll(CancellationToken cancellationToken = default, params object[] parameters)
    {
        await TestAAA(cancellationToken);
    }
    private static async Task TestAAA(CancellationToken cancellationToken = default)
    {
        var code = Intel.AAA(Intel.OperatingMode.Protected);
        var decoder = Decoder.Create(32, code);
        var instruction = decoder.Decode();
        if (instruction.Code != Code.Aaa)
            throw new Exception($"test failed for instruction {nameof(Intel.AAA)}");
        await Task.CompletedTask;
    }
}