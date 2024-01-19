using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel
{
    public class InstructionIntelBitMode(int value) : IInstructionIntelBitMode
    {
        public int Value { get; private set; } = value;
    }
}