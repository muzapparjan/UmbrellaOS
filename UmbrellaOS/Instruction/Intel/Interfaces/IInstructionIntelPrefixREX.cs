namespace UmbrellaOS.Instruction.Intel.Interfaces
{
    /**
     * <summary>
     * REX prefixes are a set of 16 opcodes that span one row of the opcode map and occupy entries 40H to 4FH.<br/>
     * These opcodes represent valid instructions (INC or DEC) in IA-32 operating modes and in compatibility mode.<br/>
     * In 64-bit mode, the same opcodes represent the instruction prefix REX and are not treated as individual instructions.
     * </summary>
     * <seealso cref="IInstructionIntelPrefix"/>
     */
    public interface IInstructionIntelPrefixREX : IInstructionIntelPrefix
    {
    }
}