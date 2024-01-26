using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * Type of partition.<br/>
     * The type is a byte value.
     * <br/><br/>
     * Unique types defined by the UEFI specification:<br/>
     * * 0xEF (i.e., UEFI System Partition) defines a UEFI system partition.<br/>
     * * 0xEE (i.e., GPT Protective) is used by a protective MBR
     * to define a fake partition covering the entire disk.
     * <br/><br/>
     * Other values are used by legacy operating systems, 
     * and are allocated independently of the UEFI specification.
     * <br/><br/>
     * Note:<br/>
     * “Partition types” by Andries Brouwer: See “Links to UEFI-Related Documents”
     * (http://uefi.org/uefi) under the heading “OS Type values used in the MBR disk layout”.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public class OSType(byte value) : IOSType
    {
        public byte Value { get; private set; } = value;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(Value, cancellationToken);
        }
    }
}