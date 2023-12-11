﻿namespace UmbrellaOS.Boot.Interfaces
{
    /**
     * <summary>
     * The MBR contains four partition records
     * that each define the beginning and ending LBAs that a partition consumes on a disk.
     * </summary>
     * <seealso cref="IMBRPartitionRecord"/>
     */
    public interface ILegacyMBRPartitionRecord : IMBRPartitionRecord
    {
    }
}