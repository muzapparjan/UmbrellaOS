﻿using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Boot.OSTypes;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot
{
    /**
     * <summary>
     * The MBR contains four partition records
     * that each define the beginning and ending LBAs that a partition consumes on a disk.
     * </summary>
     * <seealso cref="MBRPartitionRecord"/>
     */
    public sealed class LegacyMBRPartitionRecord : MBRPartitionRecord
    {
        /**
         * <summary>Create a new LegacyMBRPartitionRecord with default values.</summary>
         * <seealso cref="MBRPartitionRecord"/>
         */
        public LegacyMBRPartitionRecord() : base()
        {
            BootIndicator = 0x80;
            OSType = OSTypeUEFISystemPartition.Default;
        }
        /**
         * <summary>Create a new LegacyMBRPartitionRecord with given values.</summary>
         * <param name="startingCHS">
         * [ LegacyMBRPartitionRecord ]:<br/>
         * Start of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ ProtectiveMBRPartitionRecord ]:<br/>
         * Set to LBA 0x000200 / CHS (0, 8, 0), corresponding to the Starting LBA field.
         * </param>
         * <param name="endingCHS">
         * [ LegacyMBRPartitionRecord ]:<br/>
         * End of partition in CHS address format.<br/>
         * This field shall not be used by UEFI firmware.
         * <br/><br/>
         * [ ProtectiveMBRPartitionRecord ]:<br/>
         * Set to the CHS address of the last logical block on the disk.<br/>
         * Set to LBA 0xFFFFFF / CHS (1023, 255, 63) if it is not possible to represent the value in this field.
         * </param>
         * <param name="startingLBA">
         * [ LegacyMBRPartitionRecord ]:<br/>
         * Starting LBA of the partition on the disk.<br/>
         * This field is used by UEFI firmware to determine the start of the partition.
         * <br/><br/>
         * [ ProtectiveMBRPartitionRecord ]:<br/>
         * Set to 0x00000001 (i.e., the LBA of the GPT Partition Header).
         * </param>
         * <param name="sizeInLBA">
         * [ LegacyMBRPartitionRecord ]:<br/>
         * Size of the partition in LBA units of logical blocks.<br/>
         * This field is used by UEFI firmware to determine the size of the partition.
         * <br/><br/>
         * [ ProtectiveMBRPartitionRecord ]:<br/>
         * Set to the size of the disk minus one.<br/>
         * Set to 0xFFFFFFFF if the size of the disk is too large to be represented in this field.
         * </param>
         * <seealso cref="MBRPartitionRecord"/>
         */
        public LegacyMBRPartitionRecord(ICHS startingCHS, ICHS endingCHS, ILBA startingLBA, uint sizeInLBA) :
            base(0x80, startingCHS, OSTypeUEFISystemPartition.Default, endingCHS, startingLBA, sizeInLBA)
        { }
    }
}