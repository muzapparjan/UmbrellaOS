using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 65H—GS segment override prefix (use with any branch instruction is reserved).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixSegmentOverrideGS : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixSegmentOverrideGS Default = new();

        public PrefixSegmentOverrideGS() : base(0x65) { }
    }
}