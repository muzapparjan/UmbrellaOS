using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot.OSTypes
{
    /**
     * <summary>
     * Value: 0xEF<br/>
     * Partition that contains an EFI file system.<br/>
     * Bob Griswold (rogris@Exchange.Microsoft.com) writes:
     * MS plans on using EE and EF in the future for support of non-legacy BIOS booting.<br/>
     * Mark Doran (mark.doran@intel.com) adds:
     * these types are used to support the Extensible Firmware Interface specification (EFI);
     * go to developer.intel.com and search for EFI.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public sealed class OSTypeUEFISystemPartition : IOSType
    {
        public static readonly OSTypeUEFISystemPartition Default = new();

        public byte Value => 0xEF;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(Value, cancellationToken);
        }
    }
}