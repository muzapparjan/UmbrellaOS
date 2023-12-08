using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Boot.Interfaces
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
     */
    public interface IMBR : IBinaryStreamWriter
    {
    }
}