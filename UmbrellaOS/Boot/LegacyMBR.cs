using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

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
        /**
         * <summary>
         * Set to 0xAA55 (i.e., byte 510 contains 0x55 and byte 5 11 contains 0xAA).
         * </summary>
         */
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
        /**
         * <summary>
         * Unknown. This field shall not be used by UEFI firmware.
         * </summary>
         */
        public ushort Unknown
        {
            get => unknown;
            set => unknown = value;
        }
        /**
         * <summary>
         * Array of four legacy MBR partition records.
         * </summary>
         * <see cref="ILegacyMBRPartitionRecord"/>
         */
        public ILegacyMBRPartitionRecord[] PartitionRecords
        {
            get => partitionRecords;
            set => value.CopyTo(partitionRecords, 0);
        }
        /**
         * <summary>
         * The rest of the logical block, if any, is reserved.<br/>
         * The size is [Logical Block Size] - 512.
         * </summary>
         */
        public byte[] Reserved
        {
            get => reserved;
            set => value.CopyTo(reserved, 0);
        }

        private readonly byte[] bootCode = new byte[424];
        private uint uniqueMBRDiskSignature = 0;
        private ushort unknown = 0;
        private readonly ILegacyMBRPartitionRecord[] partitionRecords = new ILegacyMBRPartitionRecord[4];
        private readonly byte[] reserved;

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
         * <param name="partitionRecords">
         * Array of four legacy MBR partition records.
         * </param>
         * <param name="logicalBlockSize">
         * The rest of the logical block, if any, is reserved.
         * </param>
         * <exception cref="ArgumentException">
         * If the boot code is larger than 424 byte, or the count of partition records is more than 4, an ArgumentException may be thrown.
         * </exception>
         * <exception cref="NullReferenceException">
         * If the boot code array or the partition record array is null, an ArgumentOutOfRange exception may be thrown.
         * </exception>
         * <exception cref="ArgumentOutOfRangeException">
         * If the logical block size is less than 512 byte, an ArgumentOutOfRange exception may be thrown.
         * </exception>
         */
        public LegacyMBR(byte[] bootCode, uint uniqueMBRDiskSignature, ushort unknown, ILegacyMBRPartitionRecord[] partitionRecords, ulong logicalBlockSize)
        {
            BootCode = bootCode;
            UniqueMBRDiskSignature = uniqueMBRDiskSignature;
            Unknown = unknown;
            PartitionRecords = partitionRecords;
            reserved = new byte[logicalBlockSize - 512];
        }

        public void Write(Stream stream)
        {
            stream.Write(BootCode);
            stream.Write(UniqueMBRDiskSignature.GetBytesLittleEndian());
            stream.Write(Unknown.GetBytesLittleEndian());
            foreach (var record in PartitionRecords)
                record.Write(stream);
            stream.Write(SIGNATURE.GetBytesLittleEndian());
            if (Reserved.Length > 0)
                stream.Write(Reserved);
        }
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteAsync(bootCode, cancellationToken);
            await stream.WriteAsync(UniqueMBRDiskSignature.GetBytesLittleEndian(), cancellationToken);
            await stream.WriteAsync(Unknown.GetBytesLittleEndian(), cancellationToken);
            foreach (var record in PartitionRecords)
                await record.WriteAsync(stream, cancellationToken);
            await stream.WriteAsync(SIGNATURE.GetBytesLittleEndian(), cancellationToken);
            if (Reserved.Length > 0)
                await stream.WriteAsync(Reserved, cancellationToken);
        }
    }
}