using static UmbrellaOS.Instruction.Encoding.Intel;

namespace UmbrellaOS.Instruction.Encoding;

using UmbrellaOS.Generic.Extensions;
using B = BitMode;
using REG = GeneralPurposeRegister;
using W = OperandSize;
using S = SignExtend;
using SREG = SegmentRegister;
using EEE = SpecialPurposeRegister;
using TTTN = ConditionTest;
using D = Direction;

public static class Intel
{
    public enum BitMode
    {
        _16,
        _32,
        _64
    }
    public enum GeneralPurposeRegister
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
    public enum SegmentRegister
    {
        ES = 0,
        CS = 1,
        SS = 2,
        DS = 3,
        FS = 4,
        GS = 5,
    }
    public enum SpecialPurposeRegister
    {
        CR0 = 0, DR0 = 0,
        DR1 = 1,
        CR2 = 2, DR2 = 2,
        CR3 = 3, DR3 = 3,
        CR4 = 4,
        DR6 = 6,
        DR7 = 7,
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
    public enum Direction
    {
        SrcREG = 0, DstREG = 1,
        SrcModRMOrSIB = 1, DstModRMOrSIB = 0
    }

    public static byte[] EncodeRegister(REG register) => register switch
    {
        REG.AH => [1, 0, 0],
        REG.AL => [0, 0, 0],
        REG.AX => [0, 0, 0],
        REG.EAX => [0, 0, 0],
        REG.RAX => [0, 0, 0],
        REG.CH => [1, 0, 1],
        REG.CL => [0, 0, 1],
        REG.CX => [0, 0, 1],
        REG.ECX => [0, 0, 1],
        REG.RCX => [0, 0, 1],
        REG.DH => [1, 1, 0],
        REG.DL => [0, 1, 0],
        REG.DX => [0, 1, 0],
        REG.EDX => [0, 1, 0],
        REG.RDX => [0, 1, 0],
        REG.BH => [1, 1, 1],
        REG.BL => [0, 1, 1],
        REG.BX => [0, 1, 1],
        REG.EBX => [0, 1, 1],
        REG.RBX => [0, 1, 1],
        REG.SP => [1, 0, 0],
        REG.ESP => [1, 0, 0],
        REG.RSP => [1, 0, 0],
        REG.BP => [1, 0, 1],
        REG.EBP => [1, 0, 1],
        REG.RBP => [1, 0, 1],
        REG.SI => [1, 1, 0],
        REG.ESI => [1, 1, 0],
        REG.RSI => [1, 1, 0],
        REG.DI => [1, 1, 1],
        REG.EDI => [1, 1, 1],
        REG.RDI => [1, 1, 1],
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
    public static byte[] EncodeSegmentRegister2(SREG register) => register switch
    {
        SREG.ES => [0, 0],
        SREG.CS => [0, 1],
        SREG.SS => [1, 0],
        SREG.DS => [1, 1],
        _ => throw new ArgumentException($"failed to encode segment register {register}", nameof(register))
    };
    public static byte[] EncodeSegmentRegister3(SREG register)
    {
        var result = new byte[3];
        var value = (byte)register;
        result[0] = value.GetBitLittleEndian(2);
        result[1] = value.GetBitLittleEndian(1);
        result[2] = value.GetBitLittleEndian(0);
        return result;
    }
    public static byte[] EncodeSpecialPurposeRegister(EEE register)
    {
        var result = new byte[3];
        var value = (byte)register;
        result[0] = value.GetBitLittleEndian(2);
        result[1] = value.GetBitLittleEndian(1);
        result[2] = value.GetBitLittleEndian(0);
        return result;
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
    public static byte EncodeDirection(D direction) => (byte)direction;
}