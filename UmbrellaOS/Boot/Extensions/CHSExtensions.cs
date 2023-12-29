using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.Extensions
{
    public static class CHSExtensions
    {
        /**
         * <summary>Convert CHS to LBA</summary>
         * <param name="chs">the cylinder-head-sector address to convert</param>
         * <param name="hpc">maximum number of heads per cylinder</param>
         * <param name="spt">maximum number of sectors per track</param>
         * <seealso cref="ICHS"/>
         * <seealso cref="ILBA"/>
         */
        public static ILBA ToLBA(this ICHS chs, uint hpc, uint spt)
        {
            return new LBA((chs.Cylinder * hpc + chs.Head) * spt + chs.Sector - 1);
        }
    }
}