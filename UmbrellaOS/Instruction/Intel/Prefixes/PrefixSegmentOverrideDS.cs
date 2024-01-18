using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 3EH—DS segment override prefix (use with any branch instruction is reserved).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixSegmentOverrideDS : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixSegmentOverrideDS Default = new();

        public PrefixSegmentOverrideDS() : base(0x3E) { }
    }
}