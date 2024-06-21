using UglyToad.PdfPig;

namespace UmbrellaOS.CodeGen.Instruction.Encoding;

public static class Intel
{
    public static async Task<string> GenerateFromDocumentAsync(string docPath, CancellationToken cancellationToken = default)
    {
        var docExt = Path.GetExtension(docPath);
        if (docExt != ".pdf")
            throw new ArgumentException($"unexpected document format: {docExt}");
        if (!File.Exists(docPath))
            throw new FileNotFoundException($"file not exist: {docPath}");
        using PdfDocument document = PdfDocument.Open(docPath);
        var pageCount = document.NumberOfPages;

        //TEST
        var page = document.GetPage(615);
        var dic = page.Dictionary;
        foreach(var pair in dic.Data)
        {
            Console.WriteLine($"Key: {pair.Key}");
            var token = pair.Value;
            Console.WriteLine($"Token Type: {token.GetType()}");
            Console.WriteLine(token);
            Console.WriteLine("------------------------------");
        }

        //Test Result: Fucking Stupid Library!!! I have to write my own pdf reader.

        await Task.CompletedTask;
        return string.Empty;
    }
}