namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * For a bootable disk, a Protective MBR must be located at LBA 0
     * (i.e., the first logical block) of the disk if it is using the GPT disk layout.
     * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
     * with existing tools that do not understand GPT partition structures.
     * </summary>
     * <seealso cref="IMBR"/>
     */
    public interface IProtectiveMBR : IMBR
    {
    }
}