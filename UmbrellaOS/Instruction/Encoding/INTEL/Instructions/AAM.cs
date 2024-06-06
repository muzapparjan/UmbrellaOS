namespace UmbrellaOS.Instruction.Encoding.INTEL;

public static partial class Intel
{
    /**
     * <summary>
     * ASCII adjust AX after multiply.<br/><br/>
     * Adjusts the result of the multiplication of two unpacked BCD values
     * to create a pair of unpacked (base 10) BCD values.<br/>
     * The AX register is the implied source and destination operand for this instruction.<br/>
     * The AAM instruction is only useful when it follows an MUL instruction that multiplies
     * (binary multiplication) two unpacked BCD values and stores a word result in the AX register.<br/>
     * The AAM instruction then adjusts the contents of the AX register
     * to contain the correct 2-digit unpacked (base 10) BCD result.<br/><br/>
     * The generalized version of this instruction allows adjustment of the contents of the AX
     * to create two unpacked digits of any number base.<br/>
     * Here, the imm8 byte is set to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAM mnemonic is interpreted by all assemblers to mean adjust to ASCII (base 10) values.<br/>
     * To adjust to values in another number base,
     * the instruction must be hand coded in machine code (D4 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------AH := tempAL / imm8; (* imm8 is set to 0AH for the AAM mnemonic *)<br/>
     * --------AL := tempAL MOD imm8;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAM(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAM)} is invalid in {mode} mode");
        return [0xD4, 0x0A];
    }
    /**
     * <summary>
     * ASCII adjust AX after multiply to number base imm8.<br/><br/>
     * Adjusts the result of the multiplication of two unpacked BCD values
     * to create a pair of unpacked (base 10) BCD values.<br/>
     * The AX register is the implied source and destination operand for this instruction.<br/>
     * The AAM instruction is only useful when it follows an MUL instruction that multiplies
     * (binary multiplication) two unpacked BCD values and stores a word result in the AX register.<br/>
     * The AAM instruction then adjusts the contents of the AX register
     * to contain the correct 2-digit unpacked (base 10) BCD result.<br/><br/>
     * The generalized version of this instruction allows adjustment of the contents of the AX
     * to create two unpacked digits of any number base.<br/>
     * Here, the imm8 byte is set to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAM mnemonic is interpreted by all assemblers to mean adjust to ASCII (base 10) values.<br/>
     * To adjust to values in another number base,
     * the instruction must be hand coded in machine code (D4 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------AH := tempAL / imm8; (* imm8 is set to 0AH for the AAM mnemonic *)<br/>
     * --------AL := tempAL MOD imm8;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <param name="base">the base number</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAM(OperatingMode mode, byte @base)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAM)} is invalid in {mode} mode");
        return [0xD4, @base];
    }
}