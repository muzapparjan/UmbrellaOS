using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel
{
    public class InstructionIntelOpcode : IInstructionIntelOpcode
    {
        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}