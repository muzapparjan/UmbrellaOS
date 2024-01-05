using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.OSTypes;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * The MBR contains four partition records
     * that each define the beginning and ending LBAs that a partition consumes on a disk.
     * </summary>
     */
    public sealed class LegacyMBRPartitionRecord : ILegacyMBRPartitionRecord
    {
        /**
         * <summary>
         * If the value is true, the value of boot indicator will be set as 0x80, otherwise 0.<br/><br/>
         * [BootIndicator == 0x80] indicates that this is the bootable legacy partition.<br/>
         * Other values indicate that this is not a bootable legacy partition.<br/>
         * This field shall not be used by UEFI firmware.
         * </summary>
         */
        public bool Bootable
        {
            get => BootIndicator == 0x80;
            set => BootIndicator = (byte)(value ? 0x80 : 0);
        }
        /**
         * <summary>
         * 0x80 indicates that this is the bootable legacy partition.<br/>
         * Other values indicate that this is not a bootable legacy partition.<br/>
         * This field shall not be used by UEFI firmware.
         * </summary>
         */
        public byte BootIndicator
        {
            get => bootIndicator;
            set => bootIndicator = value;
        }
        /**
         * <summary>
         * Start of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * </summary>
         */
        public ICHS StartingCHS
        {
            get => startingCHS;
            set => startingCHS = value;
        }
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
        public IOSType OSType
        {
            get => osType;
            set => osType = value;
        }
        /**
         * <summary>
         * End of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * </summary>
         */
        public ICHS EndingCHS
        {
            get => endingCHS;
            set => endingCHS = value;
        }
        /**
         * <summary>
         * Starting LBA of the partition on the disk.<br/>
         * This field is used by UEFI firmware to determine the start of the partition.
         * </summary>
         */
        public ILBA StartingLBA
        {
            get => startingLBA;
            set => startingLBA = value;
        }
        /**
         * <summary>
         * Size of the partition in LBA units of logical blocks.<br/>
         * This field is used by UEFI firmware to determine the size of the partition.
         * </summary>
         */
        public uint SizeInLBA
        {
            get => sizeInLBA;
            set => sizeInLBA = value;
        }

        private byte bootIndicator = 0;
        private ICHS startingCHS = CHS.Default;
        private IOSType osType = OSTypeUEFISystemPartition.Default;
        private ICHS endingCHS = CHS.Default;
        private ILBA startingLBA = LBA.Default;
        private uint sizeInLBA = 0;

        public void Write(Stream stream)
        {
            stream.WriteByte(BootIndicator);
            StartingCHS.Write(stream);
            OSType.Write(stream);
            EndingCHS.Write(stream);
            StartingLBA.Write(stream);
            stream.Write(SizeInLBA.GetBytesLittleEndian());
        }
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(BootIndicator, cancellationToken);
            await StartingCHS.WriteAsync(stream, cancellationToken);
            await OSType.WriteAsync(stream, cancellationToken);
            await EndingCHS.WriteAsync(stream, cancellationToken);
            await StartingLBA.WriteAsync(stream, cancellationToken);
            await stream.WriteAsync(SizeInLBA.GetBytesLittleEndian(), cancellationToken);
        }
    }
}