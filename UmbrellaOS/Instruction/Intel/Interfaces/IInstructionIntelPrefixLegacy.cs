namespace UmbrellaOS.Instruction.Intel.Interfaces
{
    /**
     * <summary>
     * The legacy prefixes include 66H, 67H, F2H, and F3H.<br/>
     * They are optional, except when F2H, F3H, and 66H are used in instruction extensions.<br/>
     * Legacy prefixes must be placed before REX prefixes.
     * </summary>
     * <seealso cref="IInstructionIntelPrefix"/>
     */
    public interface IInstructionIntelPrefixLegacy : IInstructionIntelPrefix
    {
    }
}