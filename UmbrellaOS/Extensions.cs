using System.Buffers.Binary;

namespace UmbrellaOS
{
    public static class Extensions
    {
        public static void FillWithZero(this byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = 0;
        }
        public static byte[] GetBytesLittleEndian(this uint value)
        {
            var bytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
            return bytes;
        }
        public static byte[] GetBytesLittleEndian(this ulong value)
        {
            var bytes = new byte[8];
            BinaryPrimitives.WriteUInt64LittleEndian(bytes, value);
            return bytes;
        }
    }
}