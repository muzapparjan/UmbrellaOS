namespace UmbrellaOS
{
    public sealed class BootLoader
    {
        public InstructionSet instructionSet;
        public List<IInstruction>? instructions;

        public bool Validate()
        {
            if (instructions != null)
                foreach (var instruction in instructions)
                    if (instruction.GetInstruction() != instructionSet)
                        return false;
            return true;
        }
    }
}