using UmbrellaOS.Boot.OSTypes.Interfaces;

namespace UmbrellaOS.Boot.OSTypes
{
    /**
     * <summary>
     * Value: 0xEE<br/>
     * Indication that this legacy MBR is followed by an EFI header.<br/>
     * 0xEE (i.e., GPT Protective) is used by a protective MBR to define a fake partition covering the entire disk.
     * </summary>
     * <seealso cref="OSType"/>
     * <seealso cref="IOSTypeGPTProtective"/>
     */
    public sealed class OSTypeGPTProtective : OSType, IOSTypeGPTProtective
    {
        public OSTypeGPTProtective() : base(0xEE) { }
    }
}