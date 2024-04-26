using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.MBRPartitionRecords.Interfaces;
using UmbrellaOS.Boot.MBRs.Interfaces;

namespace UmbrellaOS.Boot.MBRs
{
    /**
     * <summary>
     * For a bootable disk, a Protective MBR must be located at LBA 0
     * (i.e., the first logical block) of the disk if it is using the GPT disk layout.
     * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
     * with existing tools that do not understand GPT partition structures.
     * </summary>
     * <seealso cref="MBR"/>
     * <seealso cref="IMBRProtective"/>
     */
    public sealed class MBRProtective : MBR, IMBRProtective
    {
        /**
         * <summary>
         * [ MBRLegacy ]:<br/>
         * Array of four legacy MBR partition records.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Array of four MBR partition records. Contains:<br/>
         * • one partition record; and<br/>
         * • three partition records each set to zero.
         * </summary>
         * <seealso cref="IMBRPartitionRecordProtective"/>
         * <seealso cref="IMBRPartitionRecord"/>
         */
        public new IMBRPartitionRecordProtective[] PartitionRecords
        {
            get
            {
                var records = new IMBRPartitionRecordProtective[4];
                for (var i = 0; i < 4; i++)
                    records[i] = (IMBRPartitionRecordProtective)partitionRecords[i];
                return records;
            }
            set
            {
                for (var i = 0; i < 4; i++)
                    partitionRecords[i] = value[i];
            }
        }
        /**
         * <summary>
         * The first and the only one partition record.
         * </summary>
         * <seealso cref="IMBRPartitionRecordProtective"/>
         * <seealso cref="IMBRPartitionRecord"/>
         */
        public IMBRPartitionRecordProtective PartitionRecord
        {
            get => PartitionRecords[0];
            set => PartitionRecords[0] = value;
        }

        /**
         * <summary>
         * Create a new protective MBR with default values.<br/>
         * For a bootable disk, a Protective MBR must be located at LBA 0
         * (i.e., the first logical block) of the disk if it is using the GPT disk layout.
         * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
         * with existing tools that do not understand GPT partition structures.
         * </summary>
         * <seealso cref="MBR"/>
         * <seealso cref="IMBRProtective"/>
         */
        public MBRProtective() : base() { }
        /**
         * <summary>
         * Create a new protective MBR with default values.<br/>
         * For a bootable disk, a Protective MBR must be located at LBA 0
         * (i.e., the first logical block) of the disk if it is using the GPT disk layout.
         * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
         * with existing tools that do not understand GPT partition structures.
         * </summary>
         * <param name="partitionRecord">
         * The first and the only one partition record.
         * </param>
         * <seealso cref="MBR"/>
         * <seealso cref="IMBRProtective"/>
         */
        public MBRProtective(IMBRPartitionRecordProtective partitionRecord) :
            base()
        {
            PartitionRecord = partitionRecord;
        }
    }
}