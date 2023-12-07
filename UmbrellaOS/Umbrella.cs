using DiscUtils.Iso9660;

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

        private static byte[] BuildSystem()
        {
            return new byte[0];
        }
    }
}