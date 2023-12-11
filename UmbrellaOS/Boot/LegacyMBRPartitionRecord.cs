using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.OSTypes;

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
            get => bootIndicator == 0x80;
            set => bootIndicator = (byte)(value ? 0x80 : 0);
        }
        /**
         * <summary>
         * Start of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * </summary>
         */
        public byte[] StartingCHS
        {
            get => startingCHS;
            set => value.CopyTo(startingCHS, 0);
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
        public byte[] EndingCHS
        {
            get => endingCHS;
            set => value.CopyTo(endingCHS, 0);
        }

        private byte bootIndicator = 0;
        private byte[] startingCHS = new byte[3];
        private IOSType osType = OSTypeUEFISystemPartition.Default;
        private byte[] endingCHS = new byte[3];
        private uint startingLBA = 0;
        private uint sizeInLBA = 0;

        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}