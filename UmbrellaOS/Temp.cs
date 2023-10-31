using System.Text;

namespace UmbrellaOS
{
    public static class Temp
    {
        public static byte[] GenerateBootLoader_x86_64()
        {
            var bytes = new byte[512];

            //bytes[0] = 0xEB;//infinite loop
            //bytes[1] = 0xFE;

            bytes[510] = 0x55;//boot loader flag(0xAA55 little endian)
            bytes[511] = 0xAA;

            return bytes;
        }

        public static string GenerateQemuTestScript_x86_64()
        {
            var builder = new StringBuilder();
            //builder.AppendLine("@echo off");
            builder.AppendLine("Powershell.exe -Command \"qemu-system-x86_64 -drive format=raw,file=bootloader_x86_64.bin\"");
            return builder.ToString();
        }
    }
}