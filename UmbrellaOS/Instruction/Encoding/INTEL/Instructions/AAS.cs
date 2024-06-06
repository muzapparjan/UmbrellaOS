namespace UmbrellaOS.Instruction.Encoding.INTEL;

public static partial class Intel
{
    /**
     * <summary>
     * ASCII adjust AL after subtraction.<br/><br/>
     * Adjusts the result of the subtraction of two unpacked BCD values
     * to create a unpacked BCD result.<br/>
     * The AL register is the implied source and destination operand for this instruction.<br/>
     * The AAS instruction is only useful when it follows a SUB instruction that subtracts
     * (binary subtraction) one unpacked BCD value from another and stores a byte result
     * in the AL register.<br/>
     * The AAA instruction then adjusts the contents of the AL register
     * to contain the correct 1-digit unpacked BCD result.<br/><br/>
     * If the subtraction produced a decimal carry, the AH register decrements by 1,
     * and the CF and AF flags are set.<br/>
     * If no decimal carry occurred, the CF and AF flags are cleared,
     * and the AH register is unchanged.<br/>
     * In either case, the AL register is left with its top four bits set to 0.<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-bit mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------IF ((AL AND 0FH) > 9) or (AF = 1)<br/>
     * ------------THEN<br/>
     * ----------------AX := AX – 6;<br/>
     * ----------------AH := AH – 1;<br/>
     * ----------------AF := 1;<br/>
     * ----------------CF := 1;<br/>
     * ----------------AL := AL AND 0FH;<br/>
     * ------------ELSE<br/>
     * ----------------CF := 0;<br/>
     * ----------------AF := 0;<br/>
     * ----------------AL := AL AND 0FH;<br/>
     * --------FI;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAS(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAS)} is invalid in {mode} mode");
        return [0x3F];
    }
}