using UmbrellaOS.Tests.T000001;

namespace UmbrellaOS.Sandbox
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var test = new TestGenerateMBRLegacy();
            var task = test.RunAsync(default, args.Cast<object>().ToArray());
            task.Wait();
            var result = task.Result;
            Console.WriteLine($"Test:\t{nameof(TestGenerateMBRLegacy)}");
            Console.WriteLine($"Result:");
            Console.WriteLine($"\tPassed:\t{result.IsPassed}");
            Console.WriteLine($"\tReturn:\t{result.ReturnValue}");
            Console.WriteLine($"\tException:\t{result.Exception}");
        }
    }
}