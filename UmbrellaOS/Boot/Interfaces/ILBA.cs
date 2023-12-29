using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * Logical block addressing (LBA) is a common scheme used for specifying
     * the location of blocks of data stored on computer storage devices,
     * generally secondary storage systems such as hard disk drives.<br/>
     * LBA is a particularly simple linear addressing scheme;
     * blocks are located by an integer index, with the first block being LBA 0, the second LBA 1, and so on.<br/>
     * The IDE standard included 22-bit LBA as an option,
     * which was further extended to 28-bit with the release of ATA-1 (1994) and
     * to 48-bit with the release of ATA-6 (2003),
     * whereas the size of entries in on-disk and in-memory data structures holding the address is typically 32 or 64 bits.<br/>
     * Most hard disk drives released after 1996 implement logical block addressing.
     * </summary>
     */
    public interface ILBA : IBinaryStreamWriter
    {
        /** <summary>the 32 bit logical block address</summary> */
        public uint Value { get; set; }
    }
}