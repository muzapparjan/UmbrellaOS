using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot
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
     * <seealso cref="ICHSAddress"/>
     */
    public struct CHSAddress : ICHSAddress
    {
        public static readonly CHSAddress Default = new(0, 0, 1);

        /**
         * <summary>
         * A hard disk consists of one or more platters with a read-write head is located on each side of the platter.<br/>
         * A vertical section on the corresponding ring across all platters and sides is called a cylinder.<br/>
         * The valid range is between 0 and 1023 cylinders.
         * </summary>
         * <exception cref="ArgumentOutOfRangeException">If the new value is not within the range of [0, 1023], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public int Cylinder
        {
            get => cylinder;
            set
            {
                if (value < 0 || value > 1023)
                    throw new ArgumentOutOfRangeException(nameof(value), "cylinder should be from 0 to 1023");
                cylinder = value;
            }
        }
        /**
         * <summary>
         * Synonymous with side.<br/>
         * The valid range is between 0 and 255 heads (formerly 0-15).
         * </summary>
         * <exception cref="ArgumentOutOfRangeException">If the new value is not within the range of [0, 255], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public int Head
        {
            get => head;
            set
            {
                if (value < 0 || value > 255)
                    throw new ArgumentOutOfRangeException(nameof(value), "head should be from 0 to 255");
                head = value;
            }
        }
        /**
         * <summary>
         * Tracks are the concentric rings and, each track is divided into multiple sectors.<br/>
         * 0 is reserved typically for the "boot sector".<br/>
         * The valid range is between 1 and 63 sectors.
         * </summary>
         * <exception cref="ArgumentOutOfRangeException">If the new value is not within the range of [1, 63], an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public int Sector
        {
            get => sector;
            set
            {
                if (value < 1 || value > 63)
                    throw new ArgumentOutOfRangeException(nameof(value), "sector should be from 1 to 63");
                sector = value;
            }
        }
        /**
         * <summary>
         * The CHS address represented with a byte sequence in little endian, by example: <br/>
         * (CCCCCCCC CCHHHHHH HHSSSSSS) in little endian: [HHSSSSSS, CCHHHHHH, CCCCCCCC]
         * </summary>
         * <exception cref="ArgumentOutOfRangeException">If the new value is not within the valid range, an ArgumentOutOfRange exception may be thrown.</exception>
         */
        public byte[] Value
        {
            get
            {
                var bytes = new byte[3];
                bytes[2] = (byte)(Cylinder >> 2);
                ((byte)Cylinder).CopyBitsLittleEndianTo(ref bytes[1], 0, 6, 2);
                ((byte)Head).CopyBitsLittleEndianTo(ref bytes[1], 2, 0, 6);
                ((byte)Head).CopyBitsLittleEndianTo(ref bytes[0], 0, 6, 2);
                ((byte)Sector).CopyBitsLittleEndianTo(ref bytes[0], 0, 0, 6);
                return bytes;
            }
            set
            {
                Cylinder = (value[2] << 2) + (value[1] >> 6);
                Head = (value[1] << 2) + (value[0] >> 6);
                Sector = value[0] & 0x3F;
            }
        }

        private int cylinder = 0;
        private int head = 0;
        private int sector = 1;

        /**
         * <summary>Create a new CHS address.</summary>
         * <param name="cylinder">
         * A hard disk consists of one or more platters with a read-write head is located on each side of the platter.<br/>
         * A vertical section on the corresponding ring across all platters and sides is called a cylinder.<br/>
         * The valid range is between 0 and 1023 cylinders.
         * </param>
         * <param name="head">
         * Synonymous with side.<br/>
         * The valid range is between 0 and 255 heads (formerly 0-15).
         * </param>
         * <param name="sector">
         * Tracks are the concentric rings and, each track is divided into multiple sectors.<br/>
         * 0 is reserved typically for the "boot sector".<br/>
         * The valid range is between 1 and 63 sectors.
         * </param>
         * <exception cref="ArgumentOutOfRangeException">
         * If values are not within the valid range, an ArgumentOutOfRange exception may be thrown.
         * </exception>
         */
        public CHSAddress(int cylinder, int head, int sector)
        {
            Cylinder = cylinder;
            Head = head;
            Sector = sector;
        }

        public void Write(Stream stream)
        {
            stream.Write(Value);
        }
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteAsync(Value, cancellationToken);
        }
    }
}