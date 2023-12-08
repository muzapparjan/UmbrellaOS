using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
     * of the disk if it is not using the GPT disk layout (i.e., if it is using the MBR disk layout).
     * The boot code on the MBR is not executed by UEFI firmware.
     * </summary>
     * <seealso cref="ILegacyMBR"/>
     * <seealso cref="IMBR"/>
     */
    public sealed class LegacyMBR : ILegacyMBR
    {
        public const ushort SIGNATURE = 0xAA55;

        /**
         * <summary>
         * x86 code used on a non-UEFI system to select an MBR partition record and
         * load the first logical block of that partition.
         * This code shall not be executed on UEFI systems.<br/>
         * The size of the code block should be 424 bytes.
         * </summary>
         */
        public byte[] BootCode
        {
            get => bootCode;
            set => value.CopyTo(bootCode, 0);
        }
        /**
         * <summary>
         * Unique Disk Signature This may be used by the OS to identify the disk from other disks in the system.
         * This value is always written by the OS and is never written by EFI firmware.
         * </summary>
         */
        public uint UniqueMBRDiskSignature
        {
            get => uniqueMBRDiskSignature;
            set => uniqueMBRDiskSignature = value;
        }
        public ushort Unknown
        {
            get => unknown;
            set => unknown = value;
        }
        public ILegacyMBRPartitionRecord[] PartitionRecords
        {
            get => partitionRecords;
            set => value.CopyTo(partitionRecords, 0);
        }

        private readonly byte[] bootCode = new byte[424];
        private uint uniqueMBRDiskSignature = 0;
        private ushort unknown = 0;
        private readonly ILegacyMBRPartitionRecord[] partitionRecords = new ILegacyMBRPartitionRecord[4];

        /**
         * <summary>
         * Legacy Master Boot Record<br/>
         * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
         * of the disk if it is not using the GPT disk layout
         * (i.e., if it is using the MBR disk layout).<br/>
         * The boot code on the MBR is not executed by UEFI firmware.
         * </summary>
         * <param name="bootCode">
         * x86 code used on a non-UEFI system to select an MBR partition record and 
         * load the first logical block of that partition.<br/>
         * This code shall not be executed on UEFI systems.<br/>
         * The size of the code block should be 424 bytes.
         * </param>
         * <param name="uniqueMBRDiskSignature">
         * Unique Disk Signature This may be used by the OS to identify the disk from other disks in the system.<br/>
         * This value is always written by the OS and is never written by EFI firmware.
         * </param>
         * <param name="unknown">
         * Unknown. This field shall not be used by UEFI firmware.
         * </param>
         */
        public LegacyMBR(byte[] bootCode, uint uniqueMBRDiskSignature, ushort unknown, ILegacyMBRPartitionRecord[] partitionRecords)
        {
            BootCode = bootCode;
            UniqueMBRDiskSignature = uniqueMBRDiskSignature;
            Unknown = unknown;
            PartitionRecords = partitionRecords;
        }

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