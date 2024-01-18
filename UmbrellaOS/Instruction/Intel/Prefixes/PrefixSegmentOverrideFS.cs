using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 64H—FS segment override prefix (use with any branch instruction is reserved).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixSegmentOverrideFS : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixSegmentOverrideFS Default = new();

        public PrefixSegmentOverrideFS() : base(0x64) { }
    }
}