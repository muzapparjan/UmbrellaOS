﻿using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot.OSTypes
{
    /**
     * <summary>
     * Value: 0xEE<br/>
     * Indication that this legacy MBR is followed by an EFI header.<br/>
     * 0xEE (i.e., GPT Protective) is used by a protective MBR to define a fake partition covering the entire disk.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public sealed class OSTypeGPTProtective : IOSType
    {
        public static readonly OSTypeGPTProtective Default = new();

        public byte Value => 0xEE;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(Value, cancellationToken);
        }
    }
}