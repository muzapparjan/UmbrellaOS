namespace UmbrellaOS.Instruction.Encoding;

public static class Intel
{
    /**
     * <summary>
     * The IA-32 architecture supports three basic operating modes:<br/>
     * protected mode, real-address mode, and system management mode.<br/><br/>
     * 
     * Reference:<br/>
     * [Intel® 64 and IA-32 Architectures Software Developer's Manual Combined Version Volume 1 Chapter 3 Section 1 (V1.3.1)]
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
         * For brevity, the 64-bit sub-mode is referred to as 64-bit mode in IA-32 architecture.
         * </summary>
         */
        _64Bit
    }

    /**
     * <summary>
     * Adjust After Addition<br/><br/>
     * Adjusts the sum of two unpacked BCD values to create an unpacked BCD result.<br/>
     * The AL register is the implied source and destination operand for this instruction.<br/>
     * The AAA instruction is only useful when it follows an ADD instruction that
     * adds (binary addition) two unpacked BCD values and stores a byte result in the AL register.<br/>
     * The AAA instruction then adjusts the contents of the AL register
     * to contain the correct 1-digit unpacked BCD result.<br/><br/>
     * If the addition produces a decimal carry, the AH register increments by 1,
     * and the CF and AF flags are set.<br/>
     * If there was no decimal carry, the CF and AF flags are cleared and
     * the AH register is unchanged.<br/>
     * In either case, bits 4 through 7 of the AL register are set to 0.
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAA(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAA)} is invalid in {mode} mode");
        return [0B00110111];
    }
}