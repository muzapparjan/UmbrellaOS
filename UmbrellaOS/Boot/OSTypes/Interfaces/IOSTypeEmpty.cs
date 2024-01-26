using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.OSTypes.Interfaces
{
    /**
     * <summary>
     * Value: 0x00<br/>
     * To be precise: this is not used to designate unused area on the disk,
     * but marks an unused partition table entry.
     * (All other fields should be zero as well.)<br/>
     * Unused area is not designated.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public interface IOSTypeEmpty : IOSType
    {
    }
}