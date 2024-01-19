namespace UmbrellaOS.Analysis.Generic.Interfaces
{
    /**
     * <summary>A task to analyze some parts of the Umbrella.</summary>
     */
    public interface IAnalysis
    {
        public Task<IAnalysisResult> AnalyzeAsync(CancellationToken cancellationToken = default, params object[] parameters);
    }
}