using UmbrellaOS.Generic.Interfaces;

namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * Type of partition.<br/>
     * The type is a byte value.
     * <br/><br/>
     * Unique types defined by the UEFI specification:<br/>
     * * 0xEF (i.e., UEFI System Partition) defines a UEFI system partition.<br/>
     * * 0xEE (i.e., GPT Protective) is used by a protective MBR
     * to define a fake partition covering the entire disk.
     * <br/><br/>
     * Other values are used by legacy operating systems, 
     * and are allocated independently of the UEFI specification.
     * <br/><br/>
     * Note:<br/>
     * “Partition types” by Andries Brouwer: See “Links to UEFI-Related Documents”
     * (http://uefi.org/uefi) under the heading “OS Type values used in the MBR disk layout”.
     * </summary>
     */
    public interface IOSType : IBinaryStreamWriter
    {
        /**
         * <summary>
         * The value of the type.<br/>
         * See “Links to UEFI-Related Documents”
         * (http://uefi.org/uefi) under the heading “OS Type values used in the MBR disk layout”.
         * </summary>
         * <seealso href="https://www.win.tue.nl/~aeb/partitions/partition_types-1.html"/>
         */
        public byte Value { get; }
    }
}