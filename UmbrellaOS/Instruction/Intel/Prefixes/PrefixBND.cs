using UmbrellaOS.Instruction.Intel.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Prefixes
{
    /**
     * <summary>
     * BND prefix is encoded using F2H if the following conditions are true:<br/>
     * -- CPUID.(EAX=07H, ECX=0):EBX.MPX[bit 14] is set.<br/>
     * -- BNDCFGU.EN and/or IA32_BNDCFGS.EN is set.<br/>
     * -- When the F2 prefix precedes a near CALL, a near RET, a near JMP,
     *    a short Jcc, or a near Jcc instruction (see Appendix E, “Intel® Memory Protection Extensions,”
     *    of the Intel® 64 and IA-32 Architectures Software Developer’s Manual, Volume 1).
     * </summary>
     * <seealso cref="InstructionIntelPrefix"/>
     * <seealso cref="IInstructionIntelPrefixLegacy"/>
     */
    public sealed class PrefixBND : InstructionIntelPrefix, IInstructionIntelPrefixLegacy
    {
        public static readonly PrefixBND Default = new();

        public PrefixBND() : base(0xF3) { }
    }
}