using Iced.Intel;
using System.Runtime.CompilerServices;
using UmbrellaOS.Instruction.Encoding.INTEL;

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
        var code = Intel.AAA(OperatingMode.Protected);
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
        testFunc(Intel.AAD(OperatingMode.Protected), 10);
        testFunc(Intel.AAD(OperatingMode.Protected, 0x10), 0x10);
        testFunc(Intel.AAD(OperatingMode.Protected, 0x37), 0x37);
        testFunc(Intel.AAD(OperatingMode.Protected, 0xFF), 0xFF);
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
        testFunc(Intel.AAM(OperatingMode.Protected), 10);
        testFunc(Intel.AAM(OperatingMode.Protected, 0x10), 0x10);
        testFunc(Intel.AAM(OperatingMode.Protected, 0x37), 0x37);
        testFunc(Intel.AAM(OperatingMode.Protected, 0xFF), 0xFF);
        await Task.CompletedTask;
    }
    private static async Task TestAAS(CancellationToken cancellationToken = default)
    {
        var code = Intel.AAS(OperatingMode.Protected);
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
        var reg8 = GeneralPurposeRegister.AH;

        //Adc_AL_imm8
        var code = Intel.ADC(imm8);
        var decoder = Decoder.Create(32, code);
        var instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_AL_imm8);
        AssertImm(instruction.Immediate8, imm8);

        //Adc_AX_imm16
        code = Intel.ADC(imm16);
        decoder = Decoder.Create(32, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_AX_imm16);
        AssertImm(instruction.Immediate16, imm16);

        //Adc_EAX_imm32
        code = Intel.ADC(imm32);
        decoder = Decoder.Create(32, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_EAX_imm32);
        AssertImm(instruction.Immediate32, imm32);

        //Adc_RAX_imm32
        code = Intel.ADC(imm32, OperatingMode._64Bit);
        decoder = Decoder.Create(64, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_RAX_imm32);
        AssertImm(instruction.Immediate32, imm32);

        //TODO
        code = Intel.ADC(imm8, reg8);
        decoder = Decoder.Create(32, code);
        instruction = decoder.Decode();
        AssertCode(instruction, Code.Adc_rm8_imm8);
        AssertImm(instruction.Immediate8, imm8);

        await Task.CompletedTask;
    }

    private static void AssertCode(Iced.Intel.Instruction instruction, Code code, [CallerMemberName] string? caller = null)
    {
        if (instruction.Code != code)
            throw new Exception($"{nameof(AssertCode)} test from {caller} failed: {instruction.Code} != {code}");
    }
    private static void AssertLockPrefix(Iced.Intel.Instruction instruction, [CallerMemberName] string? caller = null)
    {
        if (!instruction.HasLockPrefix)
            throw new Exception($"{nameof(AssertLockPrefix)} test from {caller} failed");
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