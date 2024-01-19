namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode16 : InstructionIntelBitMode
    {
        public static readonly BitMode16 Default = new();

        public BitMode16() : base(16) { }
    }
}