using UmbrellaOS.Boot.OSTypes.Interfaces;

namespace UmbrellaOS.Boot.OSTypes
{
    /**
     * <summary>
     * Value: 0x00<br/>
     * To be precise: this is not used to designate unused area on the disk,
     * but marks an unused partition table entry.
     * (All other fields should be zero as well.)<br/>
     * Unused area is not designated.
     * </summary>
     * <seealso cref="OSType"/>
     * <seealso cref="IOSTypeEmpty"/>
     */
    public sealed class OSTypeEmpty : OSType, IOSTypeEmpty
    {
        public static readonly OSTypeEmpty Default = new();

        public OSTypeEmpty() : base(0x00) { }
    }
}