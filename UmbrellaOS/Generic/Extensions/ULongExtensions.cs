using System.Buffers.Binary;

namespace UmbrellaOS.Generic.Extensions
{
    public static class ULongExtensions
    {
        /** <summary>Get bytes of a ulong value, as little endian.</summary> */
        public static byte[] GetBytesLittleEndian(this ulong value)
        {
            var bytes = new byte[8];
            BinaryPrimitives.WriteUInt64LittleEndian(bytes, value);
            return bytes;
        }
    }
}