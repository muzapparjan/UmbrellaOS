namespace UmbrellaOS.Instruction.Encoding.INTEL;

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