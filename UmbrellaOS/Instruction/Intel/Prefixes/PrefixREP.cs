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
    public sealed class PrefixREP : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixREP Default = new();

        public PrefixREP() : base(0xF3) { }
    }
}