using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 2EH—CS segment override (use with any branch instruction is reserved).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixSegmentOverrideCS : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixSegmentOverrideCS Default = new();

        public PrefixSegmentOverrideCS() : base(0x2E) { }
    }
}