using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * REPNE/REPNZ prefix is encoded using F2H.<br/>
     * Repeat-Not-Zero prefix applies only to string and input/output instructions.<br/>
     * (F2H is also used as a mandatory prefix for some instructions.)
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixREPNE : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixREPNE Default = new();

        public PrefixREPNE() : base(0xF2) { }
    }
}