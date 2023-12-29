using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.Extensions
{
    public static class LBAExtensions
    {
        /**
         * <summary>Convert LBA to CHS</summary>
         * <param name="lba">the logical block address to convert</param>
         * <param name="hpc">maximum number of heads per cylinder</param>
         * <param name="spt">maximum number of sectors per track</param>
         * <exception cref="ArgumentOutOfRangeException">
         * If values are not within the valid range, an ArgumentOutOfRange exception may be thrown.
         * </exception>
         * <seealso cref="ICHS"/>
         * <seealso cref="ILBA"/>
         */
        public static ICHS ToCHS(this ILBA lba, uint hpc, uint spt)
        {
            return new CHS(lba.Value / (hpc * spt), (lba.Value / spt) % hpc, (lba.Value % spt) + 1);
        }
    }
}