namespace UmbrellaOS.Instruction.Encoding.INTEL;

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