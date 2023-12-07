using DiscUtils.Iso9660;
using Iced.Intel;

namespace UmbrellaOS
{
    public static class Umbrella
    {
        public static void Build(Stream outputStream)
        {
            var builder = new CDBuilder();
            builder.VolumeIdentifier = "Umbrella";
            builder.AddDirectory(@"EFI");
            builder.AddDirectory(@"EFI\BOOT");
            builder.AddFile(@"EFI\BOOT\BOOTX64.EFI", BuildSystem());
            builder.Build(outputStream);
        }

        private static Stream BuildSystem()
        {
            var stream = new MemoryStream();
            var assembler = BuildAssembler();
            var codeWriter = new StreamCodeWriter(stream);
            assembler.Assemble(codeWriter, 0);
            return stream;
        }

        private static Assembler BuildAssembler()
        {
            var assembler = new Assembler(64);
            assembler.nop();
            return assembler;
        }
    }
}