namespace UmbrellaOS.Instruction.Encoding.INTEL;

public static partial class Intel
{
    /**
     * <summary>
     * ASCII adjust AL after addition.<br/><br/>
     * Adjusts the sum of two unpacked BCD values to create an unpacked BCD result.<br/>
     * The AL register is the implied source and destination operand for this instruction.<br/>
     * The AAA instruction is only useful when it follows an ADD instruction that
     * adds (binary addition) two unpacked BCD values and stores a byte result in the AL register.<br/>
     * The AAA instruction then adjusts the contents of the AL register
     * to contain the correct 1-digit unpacked BCD result.<br/><br/>
     * If the addition produces a decimal carry, the AH register increments by 1,
     * and the CF and AF flags are set.<br/>
     * If there was no decimal carry, the CF and AF flags are cleared and
     * the AH register is unchanged.<br/>
     * In either case, bits 4 through 7 of the AL register are set to 0.<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------IF ((AL AND 0FH) > 9) or (AF = 1)<br/>
     * ------------THEN<br/>
     * ----------------AX := AX + 106H;<br/>
     * ----------------AF := 1;<br/>
     * ----------------CF := 1;<br/>
     * ------------ELSE<br/>
     * ----------------AF := 0;<br/>
     * ----------------CF := 0;<br/>
     * --------FI;<br/>
     * --------AL := AL AND 0FH;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAA(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAA)} is invalid in {mode} mode");
        return [0x37];
    }
}