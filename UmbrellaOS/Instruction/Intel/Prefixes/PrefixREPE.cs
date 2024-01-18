using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * REP or REPE/REPZ is encoded using F3H.<br/>
     * The repeat prefix applies only to string and input/output instructions.<br/>
     * (F3H is also used as a mandatory prefix for some instructions.)
     * </summary>
     */
    public sealed class PrefixREPE : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixREPE Default = new();

        public PrefixREPE() : base(0xF3) { }
    }
}