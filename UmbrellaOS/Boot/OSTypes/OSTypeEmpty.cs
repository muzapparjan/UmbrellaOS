using UmbrellaOS.Boot.Interfaces;
using UmbrellaOS.Generic.Extensions;

namespace UmbrellaOS.Boot.OSTypes
{
    /**
     * <summary>
     * Value: 0x00<br/>
     * To be precise: this is not used to designate unused area on the disk,
     * but marks an unused partition table entry.
     * (All other fields should be zero as well.)<br/>
     * Unused area is not designated.
     * </summary>
     * <seealso cref="IOSType"/>
     */
    public sealed class OSTypeEmpty : IOSType
    {
        public static readonly OSTypeEmpty Default = new();

        public byte Value => 0x00;

        public void Write(Stream stream) => stream.WriteByte(Value);
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteByteAsync(Value, cancellationToken);
        }
    }
}