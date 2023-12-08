using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.OSTypes;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * The MBR contains four partition records
     * that each define the beginning and ending LBAs that a partition consumes on a disk.
     * </summary>
     */
    public sealed class LegacyMBRPartitionRecord : ILegacyMBRPartitionRecord
    {
        public bool Bootable
        {
            get => bootIndicator == 0x80;
            set => bootIndicator = (byte)(value ? 0x80 : 0);
        }
        public byte[] StartingCHS
        {
            get => startingCHS;
            set => value.CopyTo(startingCHS, 0);
        }

        private byte bootIndicator = 0;
        private byte[] startingCHS = new byte[3];
        private IOSType osType = OSTypeUEFISystemPartition.Default;
        private byte[] endingCHS = new byte[3];
        private uint startingLBA = 0;
        private uint sizeInLBA = 0;
    }
}