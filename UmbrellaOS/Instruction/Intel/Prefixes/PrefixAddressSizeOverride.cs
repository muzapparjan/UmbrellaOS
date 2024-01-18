using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 67H—Address-size override prefix.
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixAddressSizeOverride : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixAddressSizeOverride Default = new();

        public PrefixAddressSizeOverride() : base(0x67) { }
    }
}