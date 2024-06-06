namespace UmbrellaOS.Instruction.Encoding.INTEL;

/**
 * <summary>
 * The IA-32 architecture supports three basic operating modes:<br/>
 * protected mode, real-address mode, and system management mode.
 * </summary>
 */
public enum OperatingMode
{
    /**
     * <summary>
     * This mode is the native state of the processor.<br/>
     * Among the capabilities of protected mode is the ability to directly execute
     * “real-address mode” 8086 software in a protected, multi-tasking environment.<br/>
     * This feature is called virtual-8086 mode, although it is not actually a processor mode.<br/>
     * Virtual-8086 mode is actually a protected mode attribute that can be enabled for any task.
     * </summary>
     */
    Protected,
    /**
     * <summary>
     * This mode implements the programming environment of the Intel 8086 processor with extensions
     * (such as the ability to switch to protected or system management mode).<br/>
     * The processor is placed in real-address mode following power-up or a reset.
     * </summary>
     */
    RealAddress,
    /**
     * <summary>
     * This mode provides an operating system or executive with a transparent mechanism
     * for implementing platform-specific functions such as power management and system security.<br/>
     * The processor enters SMM when the external SMM interrupt pin (SMI#)
     * is activated or an SMI is received from the advanced programmable interrupt controller (APIC).<br/><br/>
     * In SMM, the processor switches to a separate address space
     * while saving the basic context of the currently running program or task.<br/>
     * SMM-specific code may then be executed transparently
     * Upon returning from SMM, the processor is placed back into its state prior
     * to the system management interrupt.<br/>
     * SMM was introduced with the Intel386™ SL and Intel486™ SL processors and
     * became a standard IA-32 feature with the Pentium processor family.
     * </summary>
     */
    SystemManagement,
    /**
     * <summary>
     * Compatibility mode permits most legacy 16-bit and 32-bit applications
     * to run without re-compilation under a 64-bit operating system.<br/>
     * For brevity, the compatibility sub-mode is referred to as compatibility mode
     * in IA-32 architecture.<br/>
     * Compatibility mode also supports all of the privilege levels
     * that are supported in 64-bit and protected modes.<br/>
     * Legacy applications that run in Virtual 8086 mode or use hardware task management
     * will not work in this mode.
     * </summary>
     */
    Compatibility,
    /**
     * <summary>
     * This mode enables a 64-bit operating system to run applications
     * written to access 64-bit linear address space.<br/>
     * For brevity, the 64-bit sub-mode is referred to as 64-bit mode in IA-32 architecture.<br/><br/>
     * 64-bit mode extends the number of general purpose registers and
     * SIMD extension registers from 8 to 16.<br/>
     * General purpose registers are widened to 64 bits.<br/>
     * The mode also introduces a new opcode prefix (REX) to access the register extensions.<br/><br/>
     * 64-bit mode is enabled by the operating system on a code-segment basis.<br/>
     * Its default address size is 64 bits and its default operand size is 32 bits.<br/>
     * The default operand size can be overridden on an instruction-by-instruction basis
     * using a REX opcode prefix in conjunction with an operand size override prefix.<br/><br/>
     * REX prefixes allow a 64-bit operand to be specified when operating in 64-bit mode.<br/>
     * By using this mechanism, many existing instructions have been promoted
     * to allow the use of 64-bit registers and 64-bit addresses.
     * </summary>
     */
    _64Bit
}