using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * REP or REPE/REPZ is encoded using F3H.<br/>
     * The repeat prefix applies only to string and input/output instructions.<br/>
     * (F3H is also used as a mandatory prefix for some instructions.)
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixREPZ : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixREPZ Default = new();

        public PrefixREPZ() : base(0xF3) { }
    }
}