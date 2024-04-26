using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot.OSTypes.Interfaces
{
    /**
     * <summary>
     * Value: 0xEE<br/>
     * Indication that this legacy MBR is followed by an EFI header.<br/>
     * 0xEE (i.e., GPT Protective) is used by a protective MBR to define a fake partition covering the entire disk.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public interface IOSTypeGPTProtective : IOSType
    {
    }
}