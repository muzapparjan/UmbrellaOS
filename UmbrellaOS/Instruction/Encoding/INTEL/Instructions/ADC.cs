using UmbrellaOS.Generic;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Instruction.Encoding.INTEL;

using REG = GeneralPurposeRegister;
using W = OperandSize;

public static partial class Intel
{
    /**
     * <summary>
     * Add with carry imm8 to AL.<br/><br/>
     * Adds the destination operand (first operand), the source operand (second operand),
     * and the carry (CF) flag and stores the result in the destination operand.<br/>
     * The destination operand can be a register or a memory location;
     * the source operand can be an immediate, a register, or a memory location.
     * (However, two memory operands cannot be used in one instruction.)<br/>
     * The state of the CF flag represents a carry from a previous addition.<br/>
     * When an immediate value is used as an operand,
     * it is sign-extended to the length of the destination operand format.<br/><br/>
     * The ADC instruction does not distinguish between signed or unsigned operands.<br/>
     * Instead, the processor evaluates the result for both data types and sets the OF and CF flags
     * to indicate a carry in the signed or unsigned result, respectively.<br/>
     * The SF flag indicates the sign of the signed result.<br/><br/>
     * The ADC instruction is usually executed as part of a multibyte or multiword addition
     * in which an ADD instruction is followed by an ADC instruction.<br/><br/>
     * This instruction can be used with a LOCK prefix to allow the instruction to be executed atomically.<br/><br/>
     * In 64-bit mode, the instruction’s default operation size is 32 bits.<br/>
     * Using a REX prefix in the form of REX.R permits access to additional registers (R8-R15).<br/>
     * Using a REX prefix in the form of REX.W promotes operation to 64 bits.<br/>
     * See the summary chart at the beginning of this section for encoding data and limits.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * DEST := DEST + SRC + CF;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="value">the value to add with carry</param>
     */
    public static byte[] ADC(byte value) => [0x14, value];
    /**
     * <summary>
     * Add with carry imm16 to AX.<br/><br/>
     * Adds the destination operand (first operand), the source operand (second operand),
     * and the carry (CF) flag and stores the result in the destination operand.<br/>
     * The destination operand can be a register or a memory location;
     * the source operand can be an immediate, a register, or a memory location.
     * (However, two memory operands cannot be used in one instruction.)<br/>
     * The state of the CF flag represents a carry from a previous addition.<br/>
     * When an immediate value is used as an operand,
     * it is sign-extended to the length of the destination operand format.<br/><br/>
     * The ADC instruction does not distinguish between signed or unsigned operands.<br/>
     * Instead, the processor evaluates the result for both data types and sets the OF and CF flags
     * to indicate a carry in the signed or unsigned result, respectively.<br/>
     * The SF flag indicates the sign of the signed result.<br/><br/>
     * The ADC instruction is usually executed as part of a multibyte or multiword addition
     * in which an ADD instruction is followed by an ADC instruction.<br/><br/>
     * This instruction can be used with a LOCK prefix to allow the instruction to be executed atomically.<br/><br/>
     * In 64-bit mode, the instruction’s default operation size is 32 bits.<br/>
     * Using a REX prefix in the form of REX.R permits access to additional registers (R8-R15).<br/>
     * Using a REX prefix in the form of REX.W promotes operation to 64 bits.<br/>
     * See the summary chart at the beginning of this section for encoding data and limits.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * DEST := DEST + SRC + CF;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="value">the value to add with carry</param>
     */
    public static byte[] ADC(ushort value) => [(byte)Prefix.OperandSizeOverride, 0x15, .. value.GetBytesLittleEndian()];
    public static byte[] ADC(uint value) => [0x15, .. value.GetBytesLittleEndian()];
    public static byte[] ADC(uint value, OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            return [REX(1, 0, 0, 0), 0x15, .. value.GetBytesLittleEndian()];
        return ADC(value);
    }
    public static byte[] ADC(byte value, REG register)
    {
        if (register == REG.AL)
            return ADC(value);
        var registerSize = register.GetOperandSize();
        var registerCode = register.ToBitArrayBigEndian();
        if (registerSize == W._8Bit)
            return [0x80, ((Bit[])[1, 1, 0, 1, 0, .. registerCode]).AsByteBigEndian(), value];
        throw new NotImplementedException();
    }
}