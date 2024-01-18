using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 3EH—Branch taken (used only with Jcc instructions).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixBranchTaken : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixBranchTaken Default = new();

        public PrefixBranchTaken() : base(0x3E) { }
    }
}