using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot.MBRs
{
    /**
     * <summary>
     * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
     * of the disk if it is not using the GPT disk layout (i.e., if it is using the MBR disk layout).<br/>
     * The boot code on the MBR is not executed by UEFI firmware.
     * </summary>
     * <seealso cref="MBR"/>
     * <seealso cref="IMBRLegacy"/>
     */
    public sealed class MBRLegacy : MBR, IMBRLegacy
    {
        /**
         * <summary>
         * [ MBRLegacy ]:<br/>
         * x86 code used on a non-UEFI system to select an MBR partition record and
         * load the first logical block of that partition.<br/>
         * This code shall not be executed on UEFI systems.<br/>
         * The size of the code block should be 424 bytes.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused by UEFI systems.
         * </summary>
         */
        public new byte[] BootCode
        {
            get => bootCode;
            set
            {
                if (value.Length > 424)
                    throw new ArgumentException("The size of the code block in legacy MBR should be at most 424 bytes.", nameof(value));
                value.CopyTo(bootCode, 0);
            }
        }
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
         * <seealso cref="IMBRPartitionRecordLegacy"/>
         * <seealso cref="IMBRPartitionRecord"/>
         */
        public new IMBRPartitionRecordLegacy[] PartitionRecords
        {
            get
            {
                var records = new IMBRPartitionRecordLegacy[4];
                for (var i = 0; i < 4; i++)
                    records[i] = (IMBRPartitionRecordLegacy)partitionRecords[i];
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
         * Create a new legacy MBR with default values.<br/>
         * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
         * of the disk if it is not using the GPT disk layout (i.e., if it is using the MBR disk layout).<br/>
         * The boot code on the MBR is not executed by UEFI firmware.
         * </summary>
         * <seealso cref="MBR"/>
         * <seealso cref="IMBRLegacy"/>
         */
        public MBRLegacy() : base() { }
        /**
         * <summary>
         * Create a new legacy MBR with default values.<br/>
         * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
         * of the disk if it is not using the GPT disk layout (i.e., if it is using the MBR disk layout).<br/>
         * The boot code on the MBR is not executed by UEFI firmware.
         * </summary>
         * <param name="bootCode">
         * [ MBRLegacy ]:<br/>
         * x86 code used on a non-UEFI system to select an MBR partition record and
         * load the first logical block of that partition.<br/>
         * This code shall not be executed on UEFI systems.<br/>
         * The size of the code block should be 424 bytes.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused by UEFI systems.
         * </param>
         * <param name="uniqueMBRDiskSignature">
         * [ MBRLegacy ]:<br/>
         * Unique Disk Signature This may be used by the OS to identify the disk from other disks in the system.<br/>
         * This value is always written by the OS and is never written by EFI firmware.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused. Set to zero.
         * </param>
         * <param name="unknown">
         * [ MBRLegacy ]:<br/>
         * Unknown. This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused. Set to zero.
         * </param>
         * <param name="partitionRecords">
         * [ MBRLegacy ]:<br/>
         * Array of four legacy MBR partition records.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Array of four MBR partition records. Contains:<br/>
         * • one partition record as defined See Table (below); and<br/>
         * • three partition records each set to zero.
         * </param>
         * <param name="reserved">
         * The rest of the logical block, if any, is reserved. Set to zero.
         * </param>
         * <seealso cref="MBR"/>
         * <seealso cref="IMBRLegacy"/>
         */
        public MBRLegacy(byte[] bootCode, uint uniqueMBRDiskSignature, ushort unknown, IMBRPartitionRecordLegacy[] partitionRecords, byte[] reserved) :
            base(bootCode, uniqueMBRDiskSignature, unknown, partitionRecords, reserved)
        {
            BootCode = bootCode;
            PartitionRecords = partitionRecords;
        }
    }
}