using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * For a bootable disk, a Protective MBR must be located at LBA 0
     * (i.e., the first logical block) of the disk if it is using the GPT disk layout.
     * The Protective MBR precedes the GUID Partition Table Header to maintain compatibility
     * with existing tools that do not understand GPT partition structures.
     * </summary>
     * <seealso cref="IProtectiveMBR"/>
     */
    public sealed class ProtectiveMBR : IProtectiveMBR
    {
        /**
         * <summary>
         * Set to 0xAA55 (i.e., byte 510 contains 0x55 and byte 5 11 contains 0xAA).
         * </summary>
         */
        public const ushort SIGNATURE = 0xAA55;

        private readonly byte[] bootCode = new byte[440];
        private uint uniqueMBRDiskSignature = 0;
        private ushort unknown = 0;
        private readonly IProtectiveMBRPartitionRecord[] partitionRecords = new IProtectiveMBRPartitionRecord[4];
        private readonly byte[] reserved;

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