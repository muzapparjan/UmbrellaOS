using System.Diagnostics;
using System.Text;
using UmbrellaOS.Boot;
using UmbrellaOS.Boot.Extensions;
using UmbrellaOS.Boot.MBRPartitinoRecords.Interfaces;
using UmbrellaOS.Boot.MBRPartitionRecords;
using UmbrellaOS.Boot.MBRs;
using UmbrellaOS.Tests.Generic;
using UmbrellaOS.Tests.Generic.Interfaces;

namespace UmbrellaOS.Tests.T000001
{
    public sealed class TestGenerateMBRLegacy : ITest
    {
        public async Task<ITestResult> RunAsync(CancellationToken cancellationToken = default, params object[] parameters)
        {
            try
            {
                var mbr = new MBRLegacy();
                string path = "out";
                foreach (var parameter in parameters)
                {
                    if (parameter is string s && s.StartsWith("--output="))
                    {
                        var newPath = s["--output=".Length..];
                        if (string.IsNullOrEmpty(newPath))
                            throw new ArgumentException($"invalid output path argument");
                        path = newPath;
                    }
                }

                var partitionRecords = new IMBRPartitionRecordLegacy[4];
                for (uint i = 0; i < 4; i++)
                {
                    var lbaStart = new LBA(512 + i * 1024);
                    var lbaEnd = new LBA(512 + (i + 1) * 1024 - 1);
                    partitionRecords[i] = new MBRPartitionRecordLegacy(
                        lbaStart.ToCHS(256, 64),
                        lbaEnd.ToCHS(256, 64),
                        lbaStart,
                        1024);
                }
                mbr.PartitionRecords = partitionRecords;
                mbr.BootCode = GenerateBootCodeX86();

                var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                await mbr.WriteAsync(stream, cancellationToken);
                await stream.WriteAsync(new byte[1024 * 4], cancellationToken);
                stream.Close();

                var qemuProcess = Process.Start(new ProcessStartInfo()
                {
                    FileName = "qemu-system-x86_64",
                    WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(path)),
                    Arguments = path,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }) ?? throw new Exception("Failed to start the qemu process");
                qemuProcess.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                qemuProcess.ErrorDataReceived += (s, e) => throw new Exception($"Error occured while calling qemu:\n{e.Data}");
                await qemuProcess.WaitForExitAsync(cancellationToken);

                return new GenericTestResult(true, null, null);
            }
            catch (Exception exception)
            {
                return new GenericTestResult(false, null, exception);
            }
        }
        private static byte[] GenerateBootCodeX86()
        {
            var codes = new byte[424];

            #region Create the banner

            var bannerBuilder = new StringBuilder();
            bannerBuilder.Append("        uuUUUUUUUUuu\r\n");
            bannerBuilder.Append("   uuUUUUUUUUUUUUUUUUUuu\r\n");
            bannerBuilder.Append("  uUUUUUUUUUUUUUUUUUUUUUu\r\n");
            bannerBuilder.Append("uUUUUUUUUUUUUUUUUUUUUUUUUUu\r\n");
            bannerBuilder.Append("uUUUUUUUUUUUUUUUUUUUUUUUUUu\r\n");
            bannerBuilder.Append("uUUUU       UUU       UUUUu\r\n");
            bannerBuilder.Append(" UUU        uUu        UUU\r\n");
            bannerBuilder.Append(" UUUu      uUUUu     uUUU\r\n");
            bannerBuilder.Append("  UUUUuuUUU     UUUuuUUUU\r\n");
            bannerBuilder.Append("   UUUUUUU       UUUUUUU\r\n");
            bannerBuilder.Append("     uUUUUUUUuUUUUUUUu\r\n");
            bannerBuilder.Append("         uUUUUUUUu\r\n");
            bannerBuilder.Append("       UUUUUuUuUuUUU\r\n");
            bannerBuilder.Append("         UUUUUUUUU\r\n");
            bannerBuilder.Append("     Hacked by Muzappar");
            var banner = bannerBuilder.ToString().ToCharArray();

            #endregion

            #region Set background and foreground colour

            //Reference: https://www.ired.team/miscellaneous-reversing-forensics/windows-kernel-internals/writing-a-custom-bootloader#baking-bootloader-to-usb-key--ascii-art

            //mov ah, 0x06
            //Clear / scroll screen up function
            codes[0x00] = 0xB4;
            codes[0x01] = 0x06;

            //xor al, al
            //Number of lines by which to scroll up (00h = clear entire window)
            codes[0x02] = 0x30;
            codes[0x03] = 0xC0;

            //xor cx, cx
            //Row,column of window's upper left corner
            codes[0x04] = 0x31;
            codes[0x05] = 0xC9;

            //mov dx, 0x184f
            //Row,column of window's lower right corner
            codes[0x06] = 0xBA;
            codes[0x07] = 0x4F;
            codes[0x08] = 0x18;

            //mov bh, 0x4e
            //Background/foreground colour. In our case - red background / yellow foreground (https://en.wikipedia.org/wiki/BIOS_color_attributes)
            codes[0x09] = 0xB7;
            codes[0x0A] = 0x4E;

            //int 0x10
            //Issue BIOS video services interrupt with function 0x06
            codes[0x0B] = 0xCD;
            codes[0x0C] = 0x10;

            #endregion

            #region Show the banner

            //mov si, bootloaderBanner
            //Move label's bootloaderBanner memory address to si
            codes[0x0D] = 0xBE;
            codes[0x0E] = 0x1C;//Temp
            codes[0x0F] = 0x7C;

            //mov ah, 0x0e
            //Put 0x0e to ah, which stands for "Write Character in TTY mode" when issuing a BIOS Video Services interrupt 0x10
            codes[0x10] = 0xB4;
            codes[0x11] = 0x0E;

            //loop:
            //lodsb
            //Load byte at address si to al
            codes[0x12] = 0xAC;

            //test al, al
            //Check if al==0 / a NULL byte, meaning end of a C string
            codes[0x13] = 0x84;
            codes[0x14] = 0xC0;

            //jz end
            //If al==0, jump to end, where the bootloader will be halted
            //???
            codes[0x15] = 0x74;
            codes[0x16] = 0x04;

            //int 0x10
            //Issue a BIOS interrupt 0x10 for video services
            codes[0x17] = 0xCD;
            codes[0x18] = 0x10;

            //jmp loop
            //Repeat
            codes[0x19] = 0xEB;
            codes[0x1A] = 0xF7;

            //end:
            //hlt
            //Halt the program until the next interrupt
            codes[0x1B] = 0xF4;

            //Attach the banner
            for (var i = 0; i < banner.Length; i++)
                codes[0x1C + i] = (byte)banner[i];

            #endregion

            return codes;
        }
    }
}