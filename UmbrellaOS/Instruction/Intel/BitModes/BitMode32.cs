namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode32 : InstructionIntelBitMode
    {
        public static readonly BitMode32 Default = new();

        public BitMode32() : base(32) { }
    }
}