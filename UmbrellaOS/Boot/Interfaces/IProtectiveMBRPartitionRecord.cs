namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * For a bootable disk, a Protective MBR must be located at LBA 0 (i.e., the first logical block)
     * of the disk if it is using the GPT disk layout.<br/>
     * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
     * with existing tools that do not understand GPT partition structures.
     * </summary>
     * <seealso cref="IMBRPartitionRecord"/>
     */
    public interface IProtectiveMBRPartitionRecord : IMBRPartitionRecord
    {
    }
}