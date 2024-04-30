namespace UmbrellaOS.Analysis.Generic.Interfaces
{
    /**
     * <summary>
     * Result of the analysis.
     * </summary>
     */
    public interface IAnalysisResult
    {
        public bool Success { get; }
        public object? ReturnValue { get; }
        public Exception? Exception { get; }
    }
}