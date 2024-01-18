using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * Operand-size override prefix is encoded using 66H
     * (66H is also used as a mandatory prefix for some instructions).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixOperandSizeOverride : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixOperandSizeOverride Default = new();

        public PrefixOperandSizeOverride() : base(0x66) { }
    }
}