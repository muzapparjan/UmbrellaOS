using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Interfaces
{
    /**
     * <summary>
     * Instruction prefixes are divided into four groups,
     * each with a set of allowable prefix codes.
     * For each instruction, it is only useful to include up to one prefix code
     * from each of the four groups (Groups 1, 2, 3, 4).
     * Groups 1 through 4 may be placed in any order relative to each other.<br/><br/>
     * - Group 1<br/>
     * --- Lock and repeat prefixes:<br/>
     * ------ LOCK prefix is encoded using F0H.<br/>
     * ------ REPNE/REPNZ prefix is encoded using F2H.
     *        Repeat-Not-Zero prefix applies only to string and input/output instructions.
     *        (F2H is also used as a mandatory prefix for some instructions.)<br/>
     * ------ REP or REPE/REPZ is encoded using F3H. The repeat prefix applies
     *        only to string and input/output instructions.
     *        (F3H is also used as a mandatory prefix for some instructions.)<br/>
     * --- BND prefix is encoded using F2H if the following conditions are true:<br/>
     * ------ CPUID.(EAX=07H, ECX=0):EBX.MPX[bit 14] is set.<br/>
     * ------ BNDCFGU.EN and/or IA32_BNDCFGS.EN is set.<br/>
     * ------ When the F2 prefix precedes a near CALL, a near RET,
     *        a near JMP, a short Jcc, or a near Jcc instruction
     *        (see Appendix E, “Intel® Memory Protection Extensions,”
     *        of the Intel® 64 and IA-32 Architectures Software Developer’s Manual, Volume 1).<br/>
     * - Group 2<br/>
     * --- Segment override prefixes:<br/>
     * ------ 2EH—CS segment override (use with any branch instruction is reserved).<br/>
     * ------ 36H—SS segment override prefix (use with any branch instruction is reserved).<br/>
     * ------ 3EH—DS segment override prefix (use with any branch instruction is reserved).<br/>
     * ------ 26H—ES segment override prefix (use with any branch instruction is reserved).<br/>
     * ------ 64H—FS segment override prefix (use with any branch instruction is reserved).<br/>
     * ------ 65H—GS segment override prefix (use with any branch instruction is reserved).<br/>
     * --- Branch hints:<br/>
     * ------ 2EH—Branch not taken (used only with Jcc instructions).<br/>
     * ------ 3EH—Branch taken (used only with Jcc instructions).<br/>
     * - Group 3<br/>
     * --- Operand-size override prefix is encoded using 66H
     * (66H is also used as a mandatory prefix for some instructions).<br/>
     * - Group 4<br/>
     * --- 67H—Address-size override prefix.<br/>
     * </summary>
     */
    public interface IInstructionIntelPrefix : IBinaryStreamWriter
    {
    }
}