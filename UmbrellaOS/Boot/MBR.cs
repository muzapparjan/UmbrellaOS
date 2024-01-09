using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * Master Boot Record
     * <br/><br/>
     * MBR is a classic method to save partition information on a hard disk.
     * It resides on the first sector of the boot disk.
     * It stores disk partition information and a bootloader program.
     * When we start a system, the BIOS scans all hard disks, detects the presence of MBR,
     * loads the bootloader program in RAM from the default boot disk,
     * executes the boot code to read the partition table,
     * identifies the /boot partition, loads the kernel in RAM, and passes control over to it.
     * <br/><br/>
     * MBR supports three types of partition: primary, extended, and logical on a single disk.
     * We can use only primary and logical partitions for data storage.
     * We cannot use the extended partition for data storage. It stores logical partitions.
     * <br/><br/>
     * Technically, MBR supports only four primary partitions numbered from 1 to 4.
     * If we need more partitions, we need to convert the last primary partition into the extended partition.
     * Inside the extended partition, we can create up to 11 logical partitions.
     * Thus, we can create a maximum of 14 usable partitions (3 primary and 11 logical) on a single disk.
     * The numbering for the logical partitions starts at 5.
     * <br/><br/>
     * Key Points
     * <br/>
     * * MBR stores partition information and bootloader.<br/>
     * * MBR uses the first sector of the hard disk to save the information.<br/>
     * * Only BIOS-based systems use MBR.<br/>
     * * UEFI-based systems do not use MBR. They use GPT to store partition information and bootloader programs.<br/>
     * * MBR can store partition information for a hard disk of up to 2 TB.<br/>
     * * MBR is non-redundant. It does not replicate the records it contains.<br/>
     * * If MBR is corrupt, the system can't use it to boot.<br/>
     * * MBR supports a maximum of 14 partitions.<br/>
     * * We can create a maximum of 4 primary partitions.
     * If we need more partitions, we need to convert the primary partition into an extended partition.<br/>
     * * Within the extended partition, we can create a maximum of 11 logical partitions.
     * <br/><br/>
     * Notes above is written by ComputerNetworkingNotes  Updated on 2023-12-04 06:00:01 IST<br/>
     * Link: https://www.computernetworkingnotes.com/networking-tutorials/master-boot-record-mbr-explained.html
     * </summary>
     * <seealso cref="IMBR"/>
     */
    public class MBR : IMBR
    {
        /**
         * <summary>
         * Set to 0xAA55 (i.e., byte 510 contains 0x55 and byte 5 11 contains 0xAA).
         * </summary>
         */
        public const ushort SIGNATURE = 0xAA55;

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
        public byte[] BootCode
        {
            get => bootCode;
            set => value.CopyTo(bootCode, 0);
        }
        /**
         * <summary>
         * [ MBRLegacy ]:<br/>
         * Unique Disk Signature This may be used by the OS to identify the disk from other disks in the system.<br/>
         * This value is always written by the OS and is never written by EFI firmware.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused. Set to zero.
         * </summary>
         */
        public uint UniqueMBRDiskSignature
        {
            get => uniqueMBRDiskSignature;
            set => uniqueMBRDiskSignature = value;
        }
        /**
         * <summary>
         * [ MBRLegacy ]:<br/>
         * Unknown. This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ MBRProtective ]:<br/>
         * Unused. Set to zero.
         * </summary>
         */
        public ushort Unknown
        {
            get => unknown;
            set => unknown = value;
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
         * <seealso cref="IMBRPartitionRecord"/>
         */
        public IMBRPartitionRecord[] PartitionRecords
        {
            get => partitionRecords;
            set => value.CopyTo(partitionRecords, 0);
        }
        /**
         * <summary>
         * The rest of the logical block, if any, is reserved. Set to zero.
         * </summary>
         */
        public byte[] Reserved
        {
            get => reserved;
            set => value.CopyTo(reserved, 0);
        }

        protected byte[] bootCode = new byte[440];
        protected uint uniqueMBRDiskSignature = 0;
        protected ushort unknown = 0;
        protected IMBRPartitionRecord[] partitionRecords = new IMBRPartitionRecord[4];
        protected byte[] reserved = Array.Empty<byte>();

        /**
         * <summary>
         * Create a new MBR with default values.
         * Master Boot Record
         * <br/><br/>
         * MBR is a classic method to save partition information on a hard disk.
         * It resides on the first sector of the boot disk.
         * It stores disk partition information and a bootloader program.
         * When we start a system, the BIOS scans all hard disks, detects the presence of MBR,
         * loads the bootloader program in RAM from the default boot disk,
         * executes the boot code to read the partition table,
         * identifies the /boot partition, loads the kernel in RAM, and passes control over to it.
         * <br/><br/>
         * MBR supports three types of partition: primary, extended, and logical on a single disk.
         * We can use only primary and logical partitions for data storage.
         * We cannot use the extended partition for data storage. It stores logical partitions.
         * <br/><br/>
         * Technically, MBR supports only four primary partitions numbered from 1 to 4.
         * If we need more partitions, we need to convert the last primary partition into the extended partition.
         * Inside the extended partition, we can create up to 11 logical partitions.
         * Thus, we can create a maximum of 14 usable partitions (3 primary and 11 logical) on a single disk.
         * The numbering for the logical partitions starts at 5.
         * <br/><br/>
         * Key Points
         * <br/>
         * * MBR stores partition information and bootloader.<br/>
         * * MBR uses the first sector of the hard disk to save the information.<br/>
         * * Only BIOS-based systems use MBR.<br/>
         * * UEFI-based systems do not use MBR. They use GPT to store partition information and bootloader programs.<br/>
         * * MBR can store partition information for a hard disk of up to 2 TB.<br/>
         * * MBR is non-redundant. It does not replicate the records it contains.<br/>
         * * If MBR is corrupt, the system can't use it to boot.<br/>
         * * MBR supports a maximum of 14 partitions.<br/>
         * * We can create a maximum of 4 primary partitions.
         * If we need more partitions, we need to convert the primary partition into an extended partition.<br/>
         * * Within the extended partition, we can create a maximum of 11 logical partitions.
         * <br/><br/>
         * Notes above is written by ComputerNetworkingNotes  Updated on 2023-12-04 06:00:01 IST<br/>
         * Link: https://www.computernetworkingnotes.com/networking-tutorials/master-boot-record-mbr-explained.html
         * </summary>
         */
        public MBR() { }
        /**
         * <summary>
         * Create a new MBR with default values.
         * Master Boot Record
         * <br/><br/>
         * MBR is a classic method to save partition information on a hard disk.
         * It resides on the first sector of the boot disk.
         * It stores disk partition information and a bootloader program.
         * When we start a system, the BIOS scans all hard disks, detects the presence of MBR,
         * loads the bootloader program in RAM from the default boot disk,
         * executes the boot code to read the partition table,
         * identifies the /boot partition, loads the kernel in RAM, and passes control over to it.
         * <br/><br/>
         * MBR supports three types of partition: primary, extended, and logical on a single disk.
         * We can use only primary and logical partitions for data storage.
         * We cannot use the extended partition for data storage. It stores logical partitions.
         * <br/><br/>
         * Technically, MBR supports only four primary partitions numbered from 1 to 4.
         * If we need more partitions, we need to convert the last primary partition into the extended partition.
         * Inside the extended partition, we can create up to 11 logical partitions.
         * Thus, we can create a maximum of 14 usable partitions (3 primary and 11 logical) on a single disk.
         * The numbering for the logical partitions starts at 5.
         * <br/><br/>
         * Key Points
         * <br/>
         * * MBR stores partition information and bootloader.<br/>
         * * MBR uses the first sector of the hard disk to save the information.<br/>
         * * Only BIOS-based systems use MBR.<br/>
         * * UEFI-based systems do not use MBR. They use GPT to store partition information and bootloader programs.<br/>
         * * MBR can store partition information for a hard disk of up to 2 TB.<br/>
         * * MBR is non-redundant. It does not replicate the records it contains.<br/>
         * * If MBR is corrupt, the system can't use it to boot.<br/>
         * * MBR supports a maximum of 14 partitions.<br/>
         * * We can create a maximum of 4 primary partitions.
         * If we need more partitions, we need to convert the primary partition into an extended partition.<br/>
         * * Within the extended partition, we can create a maximum of 11 logical partitions.
         * <br/><br/>
         * Notes above is written by ComputerNetworkingNotes  Updated on 2023-12-04 06:00:01 IST<br/>
         * Link: https://www.computernetworkingnotes.com/networking-tutorials/master-boot-record-mbr-explained.html
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
         * • one partition record; and<br/>
         * • three partition records each set to zero.
         * </param>
         * <param name="reserved">
         * The rest of the logical block, if any, is reserved. Set to zero.
         * </param>
         * <seealso cref="IMBR"/>
         * <seealso cref="IMBRPartitionRecord"/>
         */
        public MBR(byte[] bootCode, uint uniqueMBRDiskSignature, ushort unknown, IMBRPartitionRecord[] partitionRecords, byte[] reserved)
        {
            this.reserved = new byte[reserved.Length];
            BootCode = bootCode;
            UniqueMBRDiskSignature = uniqueMBRDiskSignature;
            Unknown = unknown;
            PartitionRecords = partitionRecords;
            Reserved = reserved;
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