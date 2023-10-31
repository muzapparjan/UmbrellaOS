namespace UmbrellaOS
{
    public interface IInstruction
    {
        public InstructionSet GetInstruction();
        public byte[] GetBytes();
    }
}