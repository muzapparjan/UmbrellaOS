namespace UmbrellaOS.Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("UmbrellaOS"))
                Directory.CreateDirectory("UmbrellaOS");

            var bootLoader_x86_64 = Temp.GenerateBootLoader_x86_64();
            File.WriteAllBytes("UmbrellaOS/bootloader_x86_64.bin", bootLoader_x86_64);

            var qemuTest_x86_64 = Temp.GenerateQemuTestScript_x86_64();
            File.WriteAllText("UmbrellaOS/script_qemu_test_bootloader_x86_64.bat", qemuTest_x86_64);
        }
    }
}