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
     * <seealso cref="IMBRPartitionRecord"/>
     */
    public class MBRPartitionRecord : IMBRPartitionRecord
    {
        /**
         * <summary>
         * [ MBRPartitionRecordLegacy ]:<br/>
         * 0x80 indicates that this is the bootable legacy partition.<br/>
         * Other values indicate that this is not a bootable legacy partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to 0x00 to indicate a non-bootable partition.<br/>
         * If set to any value other than 0x00 the behavior of this flag on non-UEFI systems is undefined.<br/>
         * Must be ignored by UEFI implementations.
         * </summary>
         */
        public byte BootIndicator
        {
            get => bootIndicator;
            set => bootIndicator = value;
        }
        /**
         * <summary>
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Start of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to LBA 0x000200 / CHS (0, 8, 0), corresponding to the Starting LBA field.
         * </summary>
         * <seealso cref="ICHS"/>
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
         * [ MBRPartitionRecordLegacy ]:<br/>
         * End of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to the CHS address of the last logical block on the disk.<br/>
         * Set to LBA 0xFFFFFF / CHS (1023, 255, 63) if it is not possible to represent the value in this field.
         * </summary>
         */
        public ICHS EndingCHS
        {
            get => endingCHS;
            set => endingCHS = value;
        }
        /**
         * <summary>
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Starting LBA of the partition on the disk.<br/>
         * This field is used by UEFI firmware to determine the start of the partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to 0x00000001 (i.e., the LBA of the GPT Partition Header).
         * </summary>
         */
        public ILBA StartingLBA
        {
            get => startingLBA;
            set => startingLBA = value;
        }
        /**
         * <summary>
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Size of the partition in LBA units of logical blocks.<br/>
         * This field is used by UEFI firmware to determine the size of the partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to the size of the disk minus one.<br/>
         * Set to 0xFFFFFFFF if the size of the disk is too large to be represented in this field.
         * </summary>
         */
        public uint SizeInLBA
        {
            get => sizeInLBA;
            set => sizeInLBA = value;
        }

        protected byte bootIndicator = 0;
        protected ICHS startingCHS = CHS.Default;
        protected IOSType osType = OSTypeEmpty.Default;
        protected ICHS endingCHS = CHS.Default;
        protected ILBA startingLBA = LBA.Default;
        protected uint sizeInLBA = 0;

        /**
         * <summary>Create a new MBR partition record with default values.</summary>
         */
        public MBRPartitionRecord() { }
        /**
         * <summary>Create a new MBR partition record with given values.</summary>
         * <param name="bootIndicator">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * 0x80 indicates that this is the bootable legacy partition.<br/>
         * Other values indicate that this is not a bootable legacy partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to 0x00 to indicate a non-bootable partition.<br/>
         * If set to any value other than 0x00 the behavior of this flag on non-UEFI systems is undefined.<br/>
         * Must be ignored by UEFI i mplementations.
         * </param>
         * <param name="startingCHS">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Start of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to LBA 0x000200 / CHS (0, 8, 0), corresponding to the Starting LBA field.
         * </param>
         * <param name="osType">
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
         * </param>
         * <param name="endingCHS">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * End of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to the CHS address of the last logical block on the disk.<br/>
         * Set to LBA 0xFFFFFF / CHS (1023, 255, 63) if it is not possible to represent the value in this field.
         * </param>
         * <param name="startingLBA">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Starting LBA of the partition on the disk.<br/>
         * This field is used by UEFI firmware to determine the start of the partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to 0x00000001 (i.e., the LBA of the GPT Partition Header).
         * </param>
         * <param name="sizeInLBA">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * Size of the partition in LBA units of logical blocks.<br/>
         * This field is used by UEFI firmware to determine the size of the partition.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to the size of the disk minus one.<br/>
         * Set to 0xFFFFFFFF if the size of the disk is too large to be represented in this field.
         * </param>
         */
        public MBRPartitionRecord(byte bootIndicator, ICHS startingCHS, IOSType osType, ICHS endingCHS, ILBA startingLBA, uint sizeInLBA)
        {
            BootIndicator = bootIndicator;
            StartingCHS = startingCHS;
            OSType = osType;
            EndingCHS = endingCHS;
            StartingLBA = startingLBA;
            SizeInLBA = sizeInLBA;
        }

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