namespace UmbrellaOS.Analysis.Generic.Interfaces
{
    /**
     * <summary>
     * Result of the analysis.
     * </summary>
     */
    public interface IAnalysisResult
    {
        public bool IsPassed { get; }
        public object? ReturnValue { get; }
        public Exception? Exception { get; }
    }
}