using Iced.Intel;
using System.Runtime.CompilerServices;
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
        await TestAAD(cancellationToken);
        await TestAAM(cancellationToken);
        await TestAAS(cancellationToken);
        await TestADC(cancellationToken);
    }
    private static async Task TestAAA(CancellationToken cancellationToken = default)
    {
        var code = Intel.AAA(Intel.OperatingMode.Protected);
        var decoder = Decoder.Create(32, code);
        var instruction = decoder.Decode();
        AssertCode(instruction, Code.Aaa);
        await Task.CompletedTask;
    }
    private static async Task TestAAD(CancellationToken cancellationToken = default)
    {
        var testFunc = (byte[] code, byte @base) =>
        {
            var decoder = Decoder.Create(32, code);
            var instruction = decoder.Decode();
            AssertCode(instruction, Code.Aad_imm8);
            AssertImm(instruction.Immediate8, @base);
        };
        testFunc(Intel.AAD(Intel.OperatingMode.Protected), 10);
        testFunc(Intel.AAD(Intel.OperatingMode.Protected, 0x10), 0x10);
        testFunc(Intel.AAD(Intel.OperatingMode.Protected, 0x37), 0x37);
        testFunc(Intel.AAD(Intel.OperatingMode.Protected, 0xFF), 0xFF);
        await Task.CompletedTask;
    }
    private static async Task TestAAM(CancellationToken cancellationToken = default)
    {
        var testFunc = (byte[] code, byte @base) =>
        {
            var decoder = Decoder.Create(32, code);
            var instruction = decoder.Decode();
            AssertCode(instruction, Code.Aam_imm8);
            AssertImm(instruction.Immediate8, @base);
        };
        testFunc(Intel.AAM(Intel.OperatingMode.Protected), 10);
        testFunc(Intel.AAM(Intel.OperatingMode.Protected, 0x10), 0x10);
        testFunc(Intel.AAM(Intel.OperatingMode.Protected, 0x37), 0x37);
        testFunc(Intel.AAM(Intel.OperatingMode.Protected, 0xFF), 0xFF);
        await Task.CompletedTask;
    }
    private static async Task TestAAS(CancellationToken cancellationToken = default)
    {
        var code = Intel.AAS(Intel.OperatingMode.Protected);
        var decoder = Decoder.Create(32, code);
        var instruction = decoder.Decode();
        AssertCode(instruction, Code.Aas);
        await Task.CompletedTask;
    }
    private static async Task TestADC(CancellationToken cancellationToken = default)
    {
        byte imm8 = 0x12;
        ushort imm16 = 0x1234;
        uint imm32 = 0x12345678;

        var code = Intel.ADC(imm8);
        var decoder = Decoder.Create(32, code);
        var instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_AL_imm8);
        AssertImm(instruction.Immediate8, imm8);

        //FIXME test not passed
        code = Intel.ADC(imm16);
        decoder = Decoder.Create(32, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_AX_imm16);
        AssertImm(instruction.Immediate16, imm16);

        code = Intel.ADC(imm32);
        decoder = Decoder.Create(32, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_EAX_imm32);
        AssertImm(instruction.Immediate32, imm32);

        //TODO

        await Task.CompletedTask;
    }

    private static void AssertCode(Iced.Intel.Instruction instruction, Code code, [CallerMemberName] string? caller = null)
    {
        if (instruction.Code != code)
            throw new Exception($"{nameof(AssertCode)} test from {caller} failed: {instruction.Code} != {code}");
    }
    private static void AssertImm(byte imm, byte answer, [CallerMemberName] string? caller = null)
    {
        if (imm != answer)
            throw new Exception($"{nameof(AssertImm)} test from {caller} failed: {imm} != {answer}");
    }
    private static void AssertImm(ushort imm, ushort answer, [CallerMemberName] string? caller = null)
    {
        if (imm != answer)
            throw new Exception($"{nameof(AssertImm)} test from {caller} failed: {imm} != {answer}");
    }
    private static void AssertImm(uint imm, uint answer, [CallerMemberName] string? caller = null)
    {
        if (imm != answer)
            throw new Exception($"{nameof(AssertImm)} test from {caller} failed: {imm} != {answer}");
    }
    private static void AssertImm(ulong imm, ulong answer, [CallerMemberName] string? caller = null)
    {
        if (imm != answer)
            throw new Exception($"{nameof(AssertImm)} test from {caller} failed: {imm} != {answer}");
    }
}