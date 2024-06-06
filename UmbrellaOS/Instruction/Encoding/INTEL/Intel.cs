using UmbrellaOS.Generic.Extensions;
using UmbrellaOS.Generic;

namespace UmbrellaOS.Instruction.Encoding.INTEL;

using D = Direction;
using EEE = SpecialPurposeRegister;
using REG = GeneralPurposeRegister;
using S = SignExtend;
using SREG = SegmentRegister;
using TTTN = ConditionTest;
using W = OperandSize;

public static partial class Intel
{
    /**
     * <summary>
     * Encode an REX prefix.<br/><br/>
     * REX prefixes are a set of 16 opcodes that span one row of the opcode map
     * and occupy entries 40H to 4FH.<br/>
     * These opcodes represent valid instructions (INC or DEC)
     * in IA-32 operating modes and in compatibility mode.<br/>
     * In 64-bit mode, the same opcodes represent the instruction prefix REX
     * and are not treated as individual instructions.<br/><br/>
     * The single-byte-opcode forms of the INC/DEC instructions are not available in 64-bit mode.<br/>
     * INC/DEC functionality is still available using ModR/M forms of the same instructions
     * (opcodes FF/0 and FF/1).<br/><br/>
     * Some combinations of REX prefix fields are invalid.<br/>
     * In such cases, the prefix is ignored. Some additional information follows:<br/><br/>
     * • Setting REX.W can be used to determine the operand size but does not solely determine operand width.
     * Like the 66H size prefix, 64-bit operand size override has no effect on byte-specific operations.<br/>
     * • For non-byte operations: if a 66H prefix is used with prefix (REX.W = 1), 66H is ignored.<br/>
     * • If a 66H override is used with REX and REX.W = 0, the operand size is 16 bits.<br/>
     * • REX.R modifies the ModR/M reg field when that field encodes a GPR, SSE, control or debug register.
     * REX.R is ignored when ModR/M specifies other registers or defines an extended opcode.<br/>
     * • REX.X bit modifies the SIB index field.<br/>
     * • REX.B either modifies the base in the ModR/M r/m field or SIB base field;
     * or it modifies the opcode reg field used for accessing GPRs.
     * </summary>
     * <param name="w">
     * 0 = Operand size determined by CS.D<br/>
     * 1 = 64 Bit Operand Size
     * </param>
     * <param name="r">Extension of the ModR/M reg field</param>
     * <param name="x">Extension of the SIB index field</param>
     * <param name="b">Extension of the ModR/M r/m field, SIB base field, or Opcode reg field</param>
     */
    internal static byte REX(Bit w, Bit r, Bit x, Bit b)
    {
        Bit[] bits = [0, 1, 0, 0, w, r, x, b];
        return bits.AsByteBigEndian();
    }
    /**
     * <summary>
     * Get size from register type.<br/><br/>
     * The current operand-size attribute determines whether the processor is performing 16-bit,
     * 32-bit or 64-bit operations.<br/>
     * Within the constraints of the current operand-size attribute,
     * the operand-size bit (w) can be used to indicate operations on 8-bit operands
     * or the full operand size specified with the operand-size attribute.
     * </summary>
     * <param name="reg">the target register</param>
     * <returns>the size of target register</returns>
     */
    internal static W GetOperandSize(this REG reg)
    {
        if (reg == REG.AL || reg == REG.BL || reg == REG.CL || reg == REG.DL ||
            reg == REG.AH || reg == REG.BH || reg == REG.CH || reg == REG.DH)
            return W._8Bit;
        if (reg == REG.AX || reg == REG.BX || reg == REG.CX || reg == REG.DX ||
            reg == REG.SP || reg == REG.BP || reg == REG.SI || reg == REG.DI)
            return W._16Bit;
        if (reg == REG.EAX || reg == REG.EBX || reg == REG.ECX || reg == REG.EDX ||
            reg == REG.ESP || reg == REG.EBP || reg == REG.ESI || reg == REG.EDI)
            return W._32Bit;
        if (reg == REG.RAX || reg == REG.RBX || reg == REG.RCX || reg == REG.RDX ||
            reg == REG.RSP || reg == REG.RBP || reg == REG.RSI || reg == REG.RDI)
            return W._64Bit;
        throw new NotImplementedException($"failed to get size of register {reg}");
    }
    /**
     * <summary>
     * Encode the operand size to a bit value.
     * </summary>
     * <param name="w">the operand size to be encoded</param>
     * <returns>a bit value that represents if an operand is of 8 bit length</returns>
     */
    internal static Bit ToBit(this W w) => w == W._8Bit ? (byte)0 : (byte)1;
    /**
     * <summary>Encode the sign extend option to a bit value.</summary>
     * <param name="s">the sign extend option to be encoded</param>
     * <param name="immWidth">width of the immediate data to effect on</param>
     */
    internal static Bit ToBit(this S s, W immWidth)
    {
        if (s == S.SignExtendToFill16BitOr32BitDestination && immWidth == W._8Bit)
            return 1;
        return 0;
    }
    /**
     * <summary>Encode the direction field to a bit value.</summary>
     * <param name="d">the direction to be encoded</param>
     */
    internal static Bit ToBit(this D d) => (byte)d;
    /**
     * <summary>
     * Encode a register as 3 bits in big endian order.
     * </summary>
     * <param name="reg">the register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    internal static Bit[] ToBitArrayBigEndian(this REG reg) => ((byte)reg).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a segment register as 2 bits in big endian order.</summary>
     * <param name="sreg">the segment register to be encoded</param>
     * <returns>a bit sequence of length 2 in big endian order</returns>
     * <exception cref="InvalidOperationException">An InvalidOperationException may be thrown if the register cannot be encoded into 2 bits.</exception>
     */
    internal static Bit[] ToBitArray2BigEndian(this SREG sreg)
    {
        var value = (byte)sreg;
        if (value > 3)
            throw new InvalidOperationException($"failed to encode Segment Register {sreg} into 2 bits");
        return value.GetBitsBigEndian(6, 2);
    }
    /**
     * <summary>Encode a segment register as 3 bits in big endian order.</summary>
     * <param name="sreg">the segment register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    internal static Bit[] ToBitArray3BigEndian(this SREG sreg) => ((byte)sreg).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a special purpose register as 3 bits in big endian order.</summary>
     * <param name="eee">the special purpose register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    internal static Bit[] ToBitArrayBigEndian(this EEE eee) => ((byte)eee).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a condition test field as 4 bits in big endian order.</summary>
     * <param name="tttn">the condition test field to be encoded</param>
     * <returns>a bit sequence of length 4 in big endian order</returns>
     */
    internal static Bit[] ToBitArrayBigEndian(this TTTN tttn) => ((byte)tttn).GetBitsBigEndian(4, 4);
}