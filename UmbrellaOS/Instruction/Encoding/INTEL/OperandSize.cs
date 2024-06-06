namespace UmbrellaOS.Instruction.Encoding.INTEL;

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