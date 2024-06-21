using UmbrellaOS.CodeGen.Instruction.Encoding;

namespace UmbrellaOS.CodeGen;

internal static class Program
{
    private static void Main(string[] args)
    {
        var intelDocPath = @"Intel® 64 and IA-32 Architectures Software Developer's Manual Combined Version.pdf";
        var task = Intel.GenerateFromDocumentAsync(intelDocPath);
        task.Wait();
    }
}