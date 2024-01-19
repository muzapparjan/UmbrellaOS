using UmbrellaOS.Generic.Interfaces;
using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Interfaces
{
    /**
     * <summary>
     * Machine Instructions are commands or programs written in the machine code
     * of a machine (computer) that it can recognize and execute.
     * A machine instruction consists of several bytes in memory that
     * tell the processor to perform one machine operation.<br/><br/>
     * The processor looks at machine instructions in main memory one after another and
     * performs one machine operation for each machine instruction.
     * A machine language program is the collection of machine instructions in the main memory.<br/><br/>
     * Machine code or machine language is a set of instructions executed directly
     * by a computer’s central processing unit (CPU).
     * Each instruction performs a very specific task, such as a load,
     * a jump, or an ALU operation on a unit of data in a CPU register or memory.
     * Every program directly executed by a CPU is made up of a series of such instructions.
     * </summary>
     */
    public interface IInstruction : IBinaryStreamWriter
    {
        public IInstructionIntelBitMode BitMode { get; }
        public IInstructionIntelPrefixLegacy[]? LegacyPrefixes { get; }
        public IInstructionIntelPrefixREX[]? REXPrefixes { get; }
        public IInstructionIntelOpcode Opcode { get; }
        public IInstructionIntelModRM? ModRMM { get; }
        public IInstructionIntelSIB? SIB { get; }
        public IInstructionIntelDisplacement? Displacement { get; }
        public IInstructionIntelImmediate? Immediate { get; }
    }
}