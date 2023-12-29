using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * Cylinder-Head-Sector (CHS) was an earlier method for addressing hard disks.<br/>
     * Even though CHS now no longer maintains a physical relationship with the disk's actual characteristics,
     * CHS is still used by many utilities.<br/><br/>
     * The following are some of the terms used:<br/>
     * * Tracks: the concentric rings<br/>
     * * Each track is divided into multiple sectors<br/>
     * * Cylinder: a hard disk consists of one or more platters with a read-write head is located on each side of the platter.
     * A vertical section on the corresponding ring across all platters and sides is called a cylinder.<br/><br/>
     * Sectors can be addressed by CHS coordinates (up to 8 gigabytes, at least). CHS stands for:<br/>
     * * C: cylinder, the valid range is between 0 and 1023 cylinders<br/>
     * * H: Head (synonymous with side), the valid range is between 0 and 254 heads (formerly 0-15)<br/>
     * * S: Sector, the valid range is between 1 and 63 sectors<br/><br/>
     * Of course, the indicated valid ranges do not reflect the physical facts.
     * There are no hard disks with 128 platters (0-255 heads).
     * These maximum values were once used by the BIOS to address the hard disk.
     * The hard disk controller then converts the values to the actual characteristics internally.
     * Today, CHS addressing is primarily used by partitioning utilities.
     * Thereby, the C and H values start at 0; and the S value at 1.
     * The first sector for a hard disk is thus located at CHS address 0/0/1.
     * Each sector is therefore 512 bytes (hard disks with a sector size of 4,096 bytes have been available since 2010).
     * Using 512 byte sectors, the maximum size of a hard disk would be 7,844 gigabytes using CHS addressing (1024*255*63*512 is 8,422,686,720 bytes).
     * </summary>
     */
    public interface ICHS : IBinaryStreamWriter
    {
        /**
         * <summary>
         * A hard disk consists of one or more platters with a read-write head is located on each side of the platter.<br/>
         * A vertical section on the corresponding ring across all platters and sides is called a cylinder.<br/>
         * The valid range is between 0 and 1023 cylinders.
         * </summary>
         */
        public uint Cylinder { get; set; }
        /**
         * <summary>
         * Synonymous with side.<br/>
         * The valid range is between 0 and 255 heads (formerly 0-15).
         * </summary>
         */
        public uint Head { get; set; }
        /**
         * <summary>
         * Tracks are the concentric rings and, each track is divided into multiple sectors.<br/>
         * 0 is reserved typically for the "boot sector".<br/>
         * The valid range is between 1 and 63 sectors.
         * </summary>
         */
        public uint Sector { get; set; }
        /**
         * <summary>
         * The CHS address represented with a byte sequence in little endian, by example: <br/>
         * (CCCCCCCC CCHHHHHH HHSSSSSS) in little endian: [HHSSSSSS, CCHHHHHH, CCCCCCCC]
         * </summary>
         */
        public byte[] Value { get; set; }
    }
}