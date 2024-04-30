using UmbrellaOS.Generic;
using UmbrellaOS.Generic.Extensions;
using static UmbrellaOS.Instruction.Encoding.Intel;

namespace UmbrellaOS.Instruction.Encoding;

using REG = GeneralPurposeRegister;
using SREG = SegmentRegister;
using EEE = SpecialPurposeRegister;
using W = OperandSize;
using S = SignExtend;
using TTTN = ConditionTest;
using D = Direction;

/**
 * <summary>
 * References:<br/>
 * • Intel® 64 and IA-32 Architectures Software Developer's Manual Combined Version
 * </summary>
 */
public static class Intel
{
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
    public enum GeneralPurposeRegister
    {
        AL = 0, AX = 0, EAX = 0, RAX = 0,
        CL = 1, CX = 1, ECX = 1, RCX = 1,
        DL = 2, DX = 2, EDX = 2, RDX = 2,
        BL = 3, BX = 3, EBX = 3, RBX = 3,
        AH = 4, SP = 4, ESP = 4, RSP = 4,
        CH = 5, BP = 5, EBP = 5, RBP = 5,
        DH = 6, SI = 6, ESI = 6, RSI = 6,
        BH = 7, DI = 7, EDI = 7, RDI = 7,
    }
    public enum SegmentRegister
    {
        ES = 0,
        CS = 1,
        SS = 2,
        DS = 3,
        FS = 4,
        GS = 5,
    }
    public enum SpecialPurposeRegister
    {
        CR0 = 0, DR0 = 0,
        DR1 = 1,
        CR2 = 2, DR2 = 2,
        CR3 = 3, DR3 = 3,
        CR4 = 4,
        DR6 = 6,
        DR7 = 7,
    }
    /**
     * <summary>
     * The current operand-size attribute determines whether the processor is performing 16-bit,
     * 32-bit or 64-bit operations.<br/>
     * Within the constraints of the current operand-size attribute,
     * the operand-size bit (w) can be used to indicate operations on 8-bit operands
     * or the full operand size specified with the operand-size attribute.
     * </summary>
     */
    public enum OperandSize
    {
        _8Bit,
        _16Bit,
        _32Bit,
        _64Bit,
    }
    /**
     * <summary>
     * The sign-extend (s) bit occurs in instructions with immediate data fields
     * that are being extended from 8 bits to 16 or 32 bits.
     * </summary>
     */
    public enum SignExtend
    {
        None,
        SignExtendToFill16BitOr32BitDestination,
    }
    public enum ConditionTest
    {
        /** <summary>Overflow</summary> */
        O = 0,
        /** <summary>No overflow</summary> */
        NO = 1,
        /** <summary>Below</summary> */
        B = 2,
        /** <summary>Not above or equal</summary> */
        NAE = 2,
        /** <summary>Not below</summary> */
        NB = 3,
        /** <summary>Above or equal</summary> */
        AE = 3,
        /** <summary>Equal</summary> */
        E = 4,
        /** <summary>Zero</summary> */
        Z = 4,
        /** <summary>Not equal</summary> */
        NE = 5,
        /** <summary>Not zero</summary> */
        NZ = 5,
        /** <summary>Below or equal</summary> */
        BE = 6,
        /** <summary>Not above</summary> */
        NA = 6,
        /** <summary>Not below or equal</summary> */
        NBE = 7,
        /** <summary>Above</summary> */
        A = 7,
        /** <summary>Sign</summary> */
        S = 8,
        /** <summary>Not sign</summary> */
        NS = 9,
        /** <summary>Parity</summary> */
        P = 10,
        /** <summary>Parity even</summary> */
        PE = 10,
        /** <summary>Not parity</summary> */
        NP = 11,
        /** <summary>Parity odd</summary> */
        PO = 11,
        /** <summary>Less than</summary> */
        L = 12,
        /** <summary>Not greater than or equal to</summary> */
        NGE = 12,
        /** <summary>Not less than</summary> */
        NL = 13,
        /** <summary>Greater than or equal to</summary> */
        GE = 13,
        /** <summary>Less than or equal to</summary> */
        LE = 14,
        /** <summary>Not greater than</summary> */
        NG = 14,
        /** <summary>Not less than or equal to</summary> */
        NLE = 15,
        /** <summary>Greater than</summary> */
        G = 15,
    }
    /**
     * <summary>
     * In many two-operand instructions, a direction bit (d) indicates
     * which operand is considered the source and which is the destination.
     * </summary>
     */
    public enum Direction
    {
        RegisterToModRMOrSIB = 0,
        ModRMOrSIBToRegister = 1,
    }

    /**
     * <summary>
     * ASCII adjust AL after addition.<br/><br/>
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
     * In either case, bits 4 through 7 of the AL register are set to 0.<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------IF ((AL AND 0FH) > 9) or (AF = 1)<br/>
     * ------------THEN<br/>
     * ----------------AX := AX + 106H;<br/>
     * ----------------AF := 1;<br/>
     * ----------------CF := 1;<br/>
     * ------------ELSE<br/>
     * ----------------AF := 0;<br/>
     * ----------------CF := 0;<br/>
     * --------FI;<br/>
     * --------AL := AL AND 0FH;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAA(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAA)} is invalid in {mode} mode");
        return [0x37];
    }
    /**
     * <summary>
     * ASCII adjust AX before division.<br/><br/>
     * Adjusts two unpacked BCD digits
     * (the least-significant digit in the AL register and the most-significant digit in the AH register)
     * so that a division operation performed on the result will yield a correct unpacked BCD value.<br/>
     * The AAD instruction is only useful when it precedes a DIV instruction that divides
     * (binary division) the adjusted value in the AX register by an unpacked BCD value.<br/><br/>
     * The AAD instruction sets the value in the AL register to (AL + (10 * AH)),
     * and then clears the AH register to 00H.<br/>
     * The value in the AX register is then equal to the binary equivalent
     * of the original unpacked two-digit (base 10) number in registers AH and AL.<br/><br/>
     * The generalized version of this instruction allows adjustment of two unpacked digits
     * of any number base (see the “Operation” section below),
     * by setting the imm8 byte to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAD mnemonic is interpreted by all assemblers to mean adjust ASCII (base 10) values.<br/>
     * To adjust values in another number base,
     * the instruction must be hand coded in machine code (D5 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------tempAH := AH;<br/>
     * --------AL := (tempAL + (tempAH ∗ imm8)) AND FFH;<br/>
     * --------(* imm8 is set to 0AH for the AAD mnemonic.*)<br/>
     * --------AH := 0;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAD(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAD)} is invalid in {mode} mode");
        return [0xD5, 0x0A];
    }
    /**
     * <summary>
     * ASCII adjust AX before division to number base imm8.<br/><br/>
     * Adjusts two unpacked BCD digits
     * (the least-significant digit in the AL register and the most-significant digit in the AH register)
     * so that a division operation performed on the result will yield a correct unpacked BCD value.<br/>
     * The AAD instruction is only useful when it precedes a DIV instruction that divides
     * (binary division) the adjusted value in the AX register by an unpacked BCD value.<br/><br/>
     * The AAD instruction sets the value in the AL register to (AL + (10 * AH)),
     * and then clears the AH register to 00H.<br/>
     * The value in the AX register is then equal to the binary equivalent
     * of the original unpacked two-digit (base 10) number in registers AH and AL.<br/><br/>
     * The generalized version of this instruction allows adjustment of two unpacked digits
     * of any number base (see the “Operation” section below),
     * by setting the imm8 byte to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAD mnemonic is interpreted by all assemblers to mean adjust ASCII (base 10) values.<br/>
     * To adjust values in another number base,
     * the instruction must be hand coded in machine code (D5 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------tempAH := AH;<br/>
     * --------AL := (tempAL + (tempAH ∗ imm8)) AND FFH;<br/>
     * --------(* imm8 is set to 0AH for the AAD mnemonic.*)<br/>
     * --------AH := 0;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <param name="base">the base number</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAD(OperatingMode mode, byte @base)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAD)} is invalid in {mode} mode");
        return [0xD5, @base];
    }
    /**
     * <summary>
     * ASCII adjust AX after multiply.<br/><br/>
     * Adjusts the result of the multiplication of two unpacked BCD values
     * to create a pair of unpacked (base 10) BCD values.<br/>
     * The AX register is the implied source and destination operand for this instruction.<br/>
     * The AAM instruction is only useful when it follows an MUL instruction that multiplies
     * (binary multiplication) two unpacked BCD values and stores a word result in the AX register.<br/>
     * The AAM instruction then adjusts the contents of the AX register
     * to contain the correct 2-digit unpacked (base 10) BCD result.<br/><br/>
     * The generalized version of this instruction allows adjustment of the contents of the AX
     * to create two unpacked digits of any number base.<br/>
     * Here, the imm8 byte is set to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAM mnemonic is interpreted by all assemblers to mean adjust to ASCII (base 10) values.<br/>
     * To adjust to values in another number base,
     * the instruction must be hand coded in machine code (D4 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------AH := tempAL / imm8; (* imm8 is set to 0AH for the AAM mnemonic *)<br/>
     * --------AL := tempAL MOD imm8;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAM(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAM)} is invalid in {mode} mode");
        return [0xD4, 0x0A];
    }
    /**
     * <summary>
     * ASCII adjust AX after multiply to number base imm8.<br/><br/>
     * Adjusts the result of the multiplication of two unpacked BCD values
     * to create a pair of unpacked (base 10) BCD values.<br/>
     * The AX register is the implied source and destination operand for this instruction.<br/>
     * The AAM instruction is only useful when it follows an MUL instruction that multiplies
     * (binary multiplication) two unpacked BCD values and stores a word result in the AX register.<br/>
     * The AAM instruction then adjusts the contents of the AX register
     * to contain the correct 2-digit unpacked (base 10) BCD result.<br/><br/>
     * The generalized version of this instruction allows adjustment of the contents of the AX
     * to create two unpacked digits of any number base.<br/>
     * Here, the imm8 byte is set to the selected number base
     * (for example, 08H for octal, 0AH for decimal, or 0CH for base 12 numbers).<br/>
     * The AAM mnemonic is interpreted by all assemblers to mean adjust to ASCII (base 10) values.<br/>
     * To adjust to values in another number base,
     * the instruction must be hand coded in machine code (D4 imm8).<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-Bit Mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------tempAL := AL;<br/>
     * --------AH := tempAL / imm8; (* imm8 is set to 0AH for the AAM mnemonic *)<br/>
     * --------AL := tempAL MOD imm8;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     * <param name="mode">target operating mode to encode</param>
     * <param name="base">the base number</param>
     * <exception cref="InvalidOperationException">an InvalidOperationException may be thrown when the operating mode is 64-bit mode(sub-mode of IA-32e mode)</exception>
     */
    public static byte[] AAM(OperatingMode mode, byte @base)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAM)} is invalid in {mode} mode");
        return [0xD4, @base];
    }
    /**
     * <summary>
     * ASCII adjust AL after subtraction.<br/><br/>
     * Adjusts the result of the subtraction of two unpacked BCD values
     * to create a unpacked BCD result.<br/>
     * The AL register is the implied source and destination operand for this instruction.<br/>
     * The AAS instruction is only useful when it follows a SUB instruction that subtracts
     * (binary subtraction) one unpacked BCD value from another and stores a byte result
     * in the AL register.<br/>
     * The AAA instruction then adjusts the contents of the AL register
     * to contain the correct 1-digit unpacked BCD result.<br/><br/>
     * If the subtraction produced a decimal carry, the AH register decrements by 1,
     * and the CF and AF flags are set.<br/>
     * If no decimal carry occurred, the CF and AF flags are cleared,
     * and the AH register is unchanged.<br/>
     * In either case, the AL register is left with its top four bits set to 0.<br/><br/>
     * This instruction executes as described in compatibility mode and legacy mode.<br/>
     * It is not valid in 64-bit mode.<br/><br/>
     * Operation:<br/>
     * --------------------------------------------------<br/>
     * IF 64-bit mode<br/>
     * ----THEN<br/>
     * --------#UD;<br/>
     * ----ELSE<br/>
     * --------IF ((AL AND 0FH) > 9) or (AF = 1)<br/>
     * ------------THEN<br/>
     * ----------------AX := AX – 6;<br/>
     * ----------------AH := AH – 1;<br/>
     * ----------------AF := 1;<br/>
     * ----------------CF := 1;<br/>
     * ----------------AL := AL AND 0FH;<br/>
     * ------------ELSE<br/>
     * ----------------CF := 0;<br/>
     * ----------------AF := 0;<br/>
     * ----------------AL := AL AND 0FH;<br/>
     * --------FI;<br/>
     * FI;<br/>
     * --------------------------------------------------
     * </summary>
     */
    public static byte[] AAS(OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            throw new InvalidOperationException($"instruction {nameof(AAS)} is invalid in {mode} mode");
        return [0x3F];
    }

    public static byte[] ADC(byte value) => [0x14, value];
    public static byte[] ADC(ushort value) => [0x66, 0x15, .. value.GetBytesLittleEndian()];
    public static byte[] ADC(uint value) => [0x15, .. value.GetBytesLittleEndian()];
    public static byte[] ADC(uint value, OperatingMode mode)
    {
        if (mode == OperatingMode._64Bit)
            return [REX(1, 0, 0, 0), 0x15, .. value.GetBytesLittleEndian()];
        return ADC(value);
    }
    public static byte[] ADC(byte value, REG register)
    {
        var registerSize = register.GetOperandSize();
        var registerCode = register.ToBitArrayBigEndian();
        if (registerSize == W._8Bit)
            return [0x80, ((Bit[])[1, 1, 0, 1, 0, .. registerCode]).AsByteBigEndian(), value];
        throw new NotImplementedException();
    }

    /**
     * <summary>
     * Encode an REX prefix.<br/><br/>
     * REX prefixes are a set of 16 opcodes that span one row of the opcode map
     * and occupy entries 40H to 4FH.<br/>
     * These opcodes represent valid instructions (INC or DEC)
     * in IA-32 operating modes and in compatibility mode.<br/>
     * In 64-bit mode, the same opcodes represent the instruction prefix REX
     * and are not treated as individual instructions.<br/><br/>
     * The single-byte-opcode forms of the INC/DEC instructions are not available in 64-bit mode.<br/>
     * INC/DEC functionality is still available using ModR/M forms of the same instructions
     * (opcodes FF/0 and FF/1).<br/><br/>
     * Some combinations of REX prefix fields are invalid.<br/>
     * In such cases, the prefix is ignored. Some additional information follows:<br/><br/>
     * • Setting REX.W can be used to determine the operand size but does not solely determine operand width.
     * Like the 66H size prefix, 64-bit operand size override has no effect on byte-specific operations.<br/>
     * • For non-byte operations: if a 66H prefix is used with prefix (REX.W = 1), 66H is ignored.<br/>
     * • If a 66H override is used with REX and REX.W = 0, the operand size is 16 bits.<br/>
     * • REX.R modifies the ModR/M reg field when that field encodes a GPR, SSE, control or debug register.
     * REX.R is ignored when ModR/M specifies other registers or defines an extended opcode.<br/>
     * • REX.X bit modifies the SIB index field.<br/>
     * • REX.B either modifies the base in the ModR/M r/m field or SIB base field;
     * or it modifies the opcode reg field used for accessing GPRs.
     * </summary>
     * <param name="w">
     * 0 = Operand size determined by CS.D<br/>
     * 1 = 64 Bit Operand Size
     * </param>
     * <param name="r">Extension of the ModR/M reg field</param>
     * <param name="x">Extension of the SIB index field</param>
     * <param name="b">Extension of the ModR/M r/m field, SIB base field, or Opcode reg field</param>
     */
    private static byte REX(Bit w, Bit r, Bit x, Bit b)
    {
        Bit[] bits = [0, 1, 0, 0, w, r, x, b];
        return bits.AsByteBigEndian();
    }

    /**
     * <summary>
     * Get size from register type.<br/><br/>
     * The current operand-size attribute determines whether the processor is performing 16-bit,
     * 32-bit or 64-bit operations.<br/>
     * Within the constraints of the current operand-size attribute,
     * the operand-size bit (w) can be used to indicate operations on 8-bit operands
     * or the full operand size specified with the operand-size attribute.
     * </summary>
     * <param name="reg">the target register</param>
     * <returns>the size of target register</returns>
     */
    private static W GetOperandSize(this REG reg)
    {
        if (reg == REG.AL || reg == REG.BL || reg == REG.CL || reg == REG.DL ||
            reg == REG.AH || reg == REG.BH || reg == REG.CH || reg == REG.DH)
            return W._8Bit;
        if (reg == REG.AX || reg == REG.BX || reg == REG.CX || reg == REG.DX ||
            reg == REG.SP || reg == REG.BP || reg == REG.SI || reg == REG.DI)
            return W._16Bit;
        if (reg == REG.EAX || reg == REG.EBX || reg == REG.ECX || reg == REG.EDX ||
            reg == REG.ESP || reg == REG.EBP || reg == REG.ESI || reg == REG.EDI)
            return W._32Bit;
        if (reg == REG.RAX || reg == REG.RBX || reg == REG.RCX || reg == REG.RDX ||
            reg == REG.RSP || reg == REG.RBP || reg == REG.RSI || reg == REG.RDI)
            return W._64Bit;
        throw new NotImplementedException($"failed to get size of register {reg}");
    }
    /**
     * <summary>
     * Encode the operand size to a bit value.
     * </summary>
     * <param name="w">the operand size to be encoded</param>
     * <returns>a bit value that represents if an operand is of 8 bit length</returns>
     */
    private static Bit ToBit(this W w) => w == W._8Bit ? (byte)0 : (byte)1;
    /**
     * <summary>Encode the sign extend option to a bit value.</summary>
     * <param name="s">the sign extend option to be encoded</param>
     * <param name="immWidth">width of the immediate data to effect on</param>
     */
    private static Bit ToBit(this S s, W immWidth)
    {
        if (s == S.SignExtendToFill16BitOr32BitDestination && immWidth == W._8Bit)
            return 1;
        return 0;
    }
    /**
     * <summary>Encode the direction field to a bit value.</summary>
     * <param name="d">the direction to be encoded</param>
     */
    private static Bit ToBit(this D d) => (byte)d;
    /**
     * <summary>
     * Encode a register as 3 bits in big endian order.
     * </summary>
     * <param name="reg">the register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    private static Bit[] ToBitArrayBigEndian(this REG reg) => ((byte)reg).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a segment register as 2 bits in big endian order.</summary>
     * <param name="sreg">the segment register to be encoded</param>
     * <returns>a bit sequence of length 2 in big endian order</returns>
     * <exception cref="InvalidOperationException">An InvalidOperationException may be thrown if the register cannot be encoded into 2 bits.</exception>
     */
    private static Bit[] ToBitArray2BigEndian(this SREG sreg)
    {
        var value = (byte)sreg;
        if (value > 3)
            throw new InvalidOperationException($"failed to encode Segment Register {sreg} into 2 bits");
        return value.GetBitsBigEndian(6, 2);
    }
    /**
     * <summary>Encode a segment register as 3 bits in big endian order.</summary>
     * <param name="sreg">the segment register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    private static Bit[] ToBitArray3BigEndian(this SREG sreg) => ((byte)sreg).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a special purpose register as 3 bits in big endian order.</summary>
     * <param name="eee">the special purpose register to be encoded</param>
     * <returns>a bit sequence of length 3 in big endian order</returns>
     */
    private static Bit[] ToBitArrayBigEndian(this EEE eee) => ((byte)eee).GetBitsBigEndian(5, 3);
    /**
     * <summary>Encode a condition test field as 4 bits in big endian order.</summary>
     * <param name="tttn">the condition test field to be encoded</param>
     * <returns>a bit sequence of length 4 in big endian order</returns>
     */
    private static Bit[] ToBitArrayBigEndian(this TTTN tttn) => ((byte)tttn).GetBitsBigEndian(4, 4);
}