using System.Buffers.Binary;

namespace UmbrellaOS.Generic.Extensions
{
    public static class UIntExtensions
    {
        /** <summary>Get bytes of a uint value, as little endian.</summary> */
        public static byte[] GetBytesLittleEndian(this uint value)
        {
            var bytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
            return bytes;
        }
    }
}