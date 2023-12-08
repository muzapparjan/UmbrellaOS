using Iced.Intel;
using System.Buffers.Binary;

namespace UmbrellaOS
{
    public static class Umbrella
    {
        public static void Test()
        {
            var outputStream = File.OpenWrite("test");
            BuildGPT(outputStream, true);
        }

        private static void BuildGPT(Stream outputStream, bool legacyMBR = false)
        {
            if (legacyMBR)
                BuildGPTLegacyMBR(outputStream);
            else
                BuildGPTProtectedMBR(outputStream);
        }
        private static void BuildGPTLegacyMBR(Stream outputStream)
        {
        }
        private static void BuildGPTProtectedMBR(Stream outputStream)
        {
        }

        private static void BuildLegacyBootCode(Stream outputStream)
        {
            var code = new byte[512];

            var memoryStream = new MemoryStream(code, 0, 424, true, true);
            var assembler = new Assembler(64);
            assembler.nop();
            assembler.jmp(0);
            assembler.Assemble(new StreamCodeWriter(memoryStream), 0);

            BinaryPrimitives.WriteUInt16LittleEndian(new Span<byte>(code, 510, 2), 0xAA55);

            outputStream.Write(code);
        }
        private static void BuildLegacyMBRPartitionRecord(Stream outputStream,
            bool bootable, byte[] startingCHS, byte osType, byte[] endingCHS, uint startingLBA, uint sizeInLBA)
        {
            outputStream.WriteByte((byte)(bootable ? 0x80 : 0));
            outputStream.Write(startingCHS);
        }
    }
}