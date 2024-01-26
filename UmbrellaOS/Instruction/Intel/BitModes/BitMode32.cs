using UmbrellaOS.Instruction.Intel.BitModes.Interfaces;

namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode32 : InstructionIntelBitMode, IBitMode32
    {
        public static readonly BitMode32 Default = new();

        public BitMode32() : base(32) { }
    }
}