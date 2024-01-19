namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode64 : InstructionIntelBitMode
    {
        public static readonly BitMode64 Default = new();

        public BitMode64() : base(64) { }
    }
}