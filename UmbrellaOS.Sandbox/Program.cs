using UmbrellaOS.Analysis.Generic.Interfaces;
using UmbrellaOS.Analysis.Project;
using UmbrellaOS.Tests.Generic.Interfaces;
using UmbrellaOS.Tests.T000001;
using UmbrellaOS.Tests.T000002;

namespace UmbrellaOS.Sandbox
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //RunAnalysis(new AnalysisLineCount(), args.Cast<object>().ToArray());

            //RunTest(new TestGenerateMBRLegacy(), args.Cast<object>().ToArray());
            RunTest(new TestEncodeInstructions(), args.Cast<object>().ToArray());
        }
        private static void RunTest(ITest test, params object[] args)
        {
            var task = test.RunAsync(default, args);
            task.Wait();
            var result = task.Result;
            Console.WriteLine($"Test:\t{test.GetType().Name}");
            Console.WriteLine($"Result:");
            Console.WriteLine($"\tPassed:\t{result.IsPassed}");
            Console.WriteLine($"\tReturn:\t{result.ReturnValue}");
            Console.WriteLine($"\tException:\t{result.Exception}");
        }
        private static void RunAnalysis(IAnalysis analysis, params object[] args)
        {
            var task = analysis.AnalyzeAsync(default, args);
            task.Wait();
            var result = task.Result;
            Console.WriteLine($"Analysis:\t{analysis.GetType().Name}");
            Console.WriteLine($"Result:");
            Console.WriteLine($"\tSuccess:\t{result.Success}");
            Console.WriteLine($"\tResult:\t{result.ReturnValue}");
            Console.WriteLine($"\tException:\t{result.Exception}");
        }
    }
}