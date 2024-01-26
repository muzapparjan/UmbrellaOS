using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.OSTypes.Interfaces
{
    /**
     * <summary>
     * Value: 0xEF<br/>
     * Partition that contains an EFI file system.<br/>
     * Bob Griswold (rogris@Exchange.Microsoft.com) writes:
     * MS plans on using EE and EF in the future for support of non-legacy BIOS booting.<br/>
     * Mark Doran (mark.doran@intel.com) adds:
     * these types are used to support the Extensible Firmware Interface specification (EFI);
     * go to developer.intel.com and search for EFI.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public interface IOSTypeUEFISystemPartition : IOSType
    {
    }
}