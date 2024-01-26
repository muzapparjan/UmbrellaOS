using UmbrellaOS.Instruction.Intel.BitModes.Interfaces;

namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode64 : InstructionIntelBitMode, IBitMod64
    {
        public static readonly BitMode64 Default = new();

        public BitMode64() : base(64) { }
    }
}