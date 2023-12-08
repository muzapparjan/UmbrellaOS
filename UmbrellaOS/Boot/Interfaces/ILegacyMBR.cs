namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * A legacy MBR may be located at LBA 0 (i.e., the first logical block)
     * of the disk if it is not using the GPT disk layout (i.e., if it is using the MBR disk layout).
     * The boot code on the MBR is not executed by UEFI firmware.
     * </summary>
     * <seealso cref="IMBR"/>
     */
    public interface ILegacyMBR : IMBR
    {
    }
}