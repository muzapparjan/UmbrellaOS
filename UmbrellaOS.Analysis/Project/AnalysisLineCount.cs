using UmbrellaOS.Analysis.Generic;
using UmbrellaOS.Analysis.Generic.Interfaces;

namespace UmbrellaOS.Analysis.Project;

public sealed class AnalysisLineCount : IAnalysis
{
    public async Task<IAnalysisResult> AnalyzeAsync(CancellationToken cancellationToken = default, params object[] parameters)
    {
        if (!SolutionUtilities.TrySearchSolutionPath(out var solutionPath))
        {
            return new GenericAnalysisResult(false, null, new Exception("failed to find solution directory"));
        }
        var solutionDir = new DirectoryInfo(solutionPath);
        var scripts = solutionDir.GetFiles("*.cs", SearchOption.AllDirectories);
        var lineCount = 0;
        foreach (var script in scripts)
        {
            var lines = File.ReadAllLines(script.FullName);
            lineCount += lines.Length;
        }
        await Task.CompletedTask;
        return new GenericAnalysisResult(true, lineCount, null);
    }
}