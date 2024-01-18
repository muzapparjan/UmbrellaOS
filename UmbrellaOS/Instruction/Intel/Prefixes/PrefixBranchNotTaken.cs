using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * 2EH—Branch not taken (used only with Jcc instructions).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixBranchNotTaken : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixBranchNotTaken Default = new();

        public PrefixBranchNotTaken() : base(0x2E) { }
    }
}