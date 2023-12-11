using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * The partition record contains partition information for the disk.
     * </summary>
     */
    public interface IMBRPartitionRecord : IBinaryStreamWriter
    {
    }
}