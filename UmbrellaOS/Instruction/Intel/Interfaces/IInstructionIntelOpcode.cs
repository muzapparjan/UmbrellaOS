using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Instruction.Intel.Interfaces
{
    /**
     * <summary>
     * A primary opcode can be 1, 2, or 3 bytes in length.<br/>
     * An additional 3-bit opcode field is sometimes encoded in the ModR/M byte.<br/>
     * Smaller fields can be defined within the primary opcode.<br/>
     * Such fields define the direction of operation, size of displacements,
     * register encoding, condition codes, or sign extension.<br/>
     * Encoding fields used by an opcode vary depending on the class of operation.<br/><br/>
     * Two-byte opcode formats for general-purpose and SIMD instructions consist of one of the following:<br/>
     * -- An escape opcode byte 0FH as the primary opcode and a second opcode byte.<br/>
     * -- A mandatory prefix (66H, F2H, or F3H), an escape opcode byte, and a second opcode byte (same as previous bullet).
     * </summary>
     */
    public interface IInstructionIntelOpcode : IBinaryStreamWriter
    {
    }
}