using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * LOCK prefix is encoded using F0H.
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixLOCK : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixLOCK Default = new();

        public PrefixLOCK() : base(0xF0) { }
    }
}