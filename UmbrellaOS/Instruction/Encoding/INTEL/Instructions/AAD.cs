namespace UmbrellaOS.Instruction.Encoding.INTEL;

public static partial class Intel
{
    /**
     * <summary>
     * ASCII adjust AX before division.<br/><br/>
     * Adjusts two unpacked BCD digits
     * (the least-significant digit in the AL register and the most-significant digit in the AH register)
     * so that a division operation performed on the result will yield a correct unpacked BCD value.<br/>
     * The AAD instruction is only useful when it precedes a DIV instruction that divides
     * (binary division) the adjusted value in the AX register by an unpacked BCD value.<br/><br/>
     * The AAD instruction sets the value in the AL register to (AL + (10 * AH)),
     * and then clears the AH register to 00H.<br/>
     * The value in the AX register is then equal to the binary equivalent
     * of the original unpacked two-digit (base 10) number in registers AH and AL.<br/><br/>
     * The generalized version of this instruction allows adjustment of two unpacked digits
     * of any number base (see the “Operation” section below),
     * by setting the imm8 byte to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAD mnemonic is interpreted by all assemblers to mean adjust ASCII (base 10) values.<br/>
     * To adjust values in another number base,
     * the instruction must be hand coded in machine code (D5 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------tempAH := AH;<br/>
     * --------AL := (tempAL + (tempAH ∗ imm8)) AND FFH;<br/>
     * --------(* imm8 is set to 0AH for the AAD mnemonic.*)<br/>
     * --------AH := 0;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAD(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAD)} is invalid in {mode} mode");
        return [0xD5, 0x0A];
    }
    /**
     * <summary>
     * ASCII adjust AX before division to number base imm8.<br/><br/>
     * Adjusts two unpacked BCD digits
     * (the least-significant digit in the AL register and the most-significant digit in the AH register)
     * so that a division operation performed on the result will yield a correct unpacked BCD value.<br/>
     * The AAD instruction is only useful when it precedes a DIV instruction that divides
     * (binary division) the adjusted value in the AX register by an unpacked BCD value.<br/><br/>
     * The AAD instruction sets the value in the AL register to (AL + (10 * AH)),
     * and then clears the AH register to 00H.<br/>
     * The value in the AX register is then equal to the binary equivalent
     * of the original unpacked two-digit (base 10) number in registers AH and AL.<br/><br/>
     * The generalized version of this instruction allows adjustment of two unpacked digits
     * of any number base (see the “Operation” section below),
     * by setting the imm8 byte to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAD mnemonic is interpreted by all assemblers to mean adjust ASCII (base 10) values.<br/>
     * To adjust values in another number base,
     * the instruction must be hand coded in machine code (D5 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------tempAH := AH;<br/>
     * --------AL := (tempAL + (tempAH ∗ imm8)) AND FFH;<br/>
     * --------(* imm8 is set to 0AH for the AAD mnemonic.*)<br/>
     * --------AH := 0;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <param name="base">the base number</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAD(OperatingMode mode, byte @base)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAD)} is invalid in {mode} mode");
        return [0xD5, @base];
    }
}