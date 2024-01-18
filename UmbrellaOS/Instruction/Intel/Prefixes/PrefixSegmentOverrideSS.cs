using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 36H—SS segment override prefix (use with any branch instruction is reserved).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixSegmentOverrideSS : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixSegmentOverrideSS Default = new();

        public PrefixSegmentOverrideSS() : base(0x36) { }
    }
}