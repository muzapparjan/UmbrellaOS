using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.MBRPartitionRecords.Interfaces;
using UmbrellaOS.Boot.OSTypes;

namespace UmbrellaOS.Boot.MBRPartitionRecords
{
    /**
     * <summary>
     * The MBR contains four partition records
     * that each define the beginning and ending LBAs that a partition consumes on a disk.
     * </summary>
     * <seealso cref="MBRPartitionRecord"/>
     */
    public sealed class MBRPartitionRecordProtective : MBRPartitionRecord, IMBRPartitionRecordProtective
    {
        /**
         * <summary>Create a new protective MBR partition record with default values.</summary>
         * <seealso cref="MBRPartitionRecord"/>
         */
        public MBRPartitionRecordProtective() : base()
        {
            BootIndicator = 0x00;
            StartingCHS = new CHS(0, 8, 0);
            OSType = OSTypeGPTProtective.Default;
            EndingCHS = new CHS(1023, 255, 63);
            StartingLBA = new LBA(0x00000001);
            SizeInLBA = 0xFFFFFFFF;
        }
        /**
         * <summary>Create a new protective MBR partition record with given values.</summary>
         * <param name="endingCHS">
         * [ MBRPartitionRecordLegacy ]:<br/>
         * End of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRPartitionRecordProtective ]:<br/>
         * Set to the CHS address of the last logical block on the disk.<br/>
         * Set to LBA 0xFFFFFF / CHS (1023, 255, 63) if it is not possible to represent the value in this field.
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
         * <seealso cref="MBRPartitionRecord"/>
         */
        public MBRPartitionRecordProtective(ICHS endingCHS, uint sizeInLBA) :
            base(0x00, new CHS(0, 8, 0), OSTypeGPTProtective.Default, endingCHS, new LBA(0x00000001), sizeInLBA)
        { }
    }
}