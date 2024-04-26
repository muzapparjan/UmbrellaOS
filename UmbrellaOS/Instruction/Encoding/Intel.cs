using static UmbrellaOS.Instruction.Encoding.Intel;

namespace UmbrellaOS.Instruction.Encoding;

using UmbrellaOS.Generic.Extensions;
using B = BitMode;
using R = Register;
using W = OperandSize;
using S = SignExtend;
using TTTN = ConditionTest;

public static class Intel
{
    public enum BitMode
    {
        _16,
        _32,
        _64
    }
    public enum Register
    {
        AH, AL, AX, EAX, RAX,
        CH, CL, CX, ECX, RCX,
        DH, DL, DX, EDX, RDX,
        BH, BL, BX, EBX, RBX,
        SP, ESP, RSP,
        BP, EBP, RBP,
        SI, ESI, RSI,
        DI, EDI, RDI,
    }
    public enum OperandSize
    {
        _8,
        _16,
        _32
    }
    public enum SignExtend
    {
        None,
        SignExtendToFill16BitOr32BitDestination
    }
    public enum ConditionTest
    {
        O = 0, Overflow = 0,
        NO = 1, NoOverflow = 1,
        B = 2, NAE = 2, Below = 2, NotAboveOrEqual = 2,
        NB = 3, AE = 3, NotBelow = 3, AboveOrEqual = 3,
        E = 4, Z = 4, Equal = 4, Zero = 4,
        NE = 5, NZ = 5, NotEqual = 5, NotZero = 5,
        BE = 6, NA = 6, BelowOrEqual = 6, NotAbove = 6,
        NBE = 7, A = 7, NotBelowOrEqual = 7, Above = 7,
        S = 8, Sign = 8,
        NS = 9, NotSign = 9,
        P = 10, PE = 10, Parity = 10, ParityEven = 10,
        NP = 11, PO = 11, NotParity = 11, ParityOdd = 11,
        L = 12, NGE = 12, LessThan = 12, NotGreaterThanOrEqualTo = 12,
        NL = 13, GE = 13, NotLessThan = 13, GreaterThanOrEqualTo = 13,
        LE = 14, NG = 14, LessThanOrEqualTo = 14, NotGreaterThan = 14,
        NLE = 15, G = 15, NotLessThanOrEqualTo = 15, GreaterThan = 15,
    }

    public static byte[] EncodeRegister(R register) => register switch
    {
        R.AH => [1, 0, 0],
        R.AL => [0, 0, 0],
        R.AX => [0, 0, 0],
        R.EAX => [0, 0, 0],
        R.RAX => [0, 0, 0],
        R.CH => [1, 0, 1],
        R.CL => [0, 0, 1],
        R.CX => [0, 0, 1],
        R.ECX => [0, 0, 1],
        R.RCX => [0, 0, 1],
        R.DH => [1, 1, 0],
        R.DL => [0, 1, 0],
        R.DX => [0, 1, 0],
        R.EDX => [0, 1, 0],
        R.RDX => [0, 1, 0],
        R.BH => [1, 1, 1],
        R.BL => [0, 1, 1],
        R.BX => [0, 1, 1],
        R.EBX => [0, 1, 1],
        R.RBX => [0, 1, 1],
        R.SP => [1, 0, 0],
        R.ESP => [1, 0, 0],
        R.RSP => [1, 0, 0],
        R.BP => [1, 0, 1],
        R.EBP => [1, 0, 1],
        R.RBP => [1, 0, 1],
        R.SI => [1, 1, 0],
        R.ESI => [1, 1, 0],
        R.RSI => [1, 1, 0],
        R.DI => [1, 1, 1],
        R.EDI => [1, 1, 1],
        R.RDI => [1, 1, 1],
        _ => throw new ArgumentException($"failed to encode register {register}", nameof(register))
    };
    public static byte EncodeOperandSize(W operandSize)
    {
        if (operandSize == W._8)
            return 0;
        return 1;
    }
    public static byte EncodeSignExtend(S signExtend, bool effectOn16Or32BitImmediateData)
    {
        if (effectOn16Or32BitImmediateData)
            return 0;
        if (signExtend == S.SignExtendToFill16BitOr32BitDestination)
            return 1;
        return 0;
    }
    public static byte[] EncodeConditionTest(TTTN conditionTest)
    {
        var result = new byte[3];
        var value = (byte)conditionTest;
        result[0] = value.GetBitLittleEndian(3);
        result[1] = value.GetBitLittleEndian(2);
        result[2] = value.GetBitLittleEndian(1);
        result[3] = value.GetBitLittleEndian(0);
        return result;
    }
}