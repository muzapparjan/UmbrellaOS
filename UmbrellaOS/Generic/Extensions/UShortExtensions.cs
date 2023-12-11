using System.Buffers.Binary;

namespace UmbrellaOS.Generic.Extensions
{
    public static class UShortExtensions
    {
        /** <summary>Get bytes of a ushort value, as little endian.</summary> */
        public static byte[] GetBytesLittleEndian(this ushort value)
        {
            var bytes = new byte[2];
            BinaryPrimitives.WriteUInt16LittleEndian(bytes, value);
            return bytes;
        }
    }
}