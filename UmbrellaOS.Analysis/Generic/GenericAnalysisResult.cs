using UmbrellaOS.Analysis.Generic.Interfaces;

namespace UmbrellaOS.Analysis.Generic
{
    /**
     * <summary>
     * Generic result of the analysis.
     * </summary>
     * <seealso cref="IAnalysisResult"/>
     */
    public sealed class GenericAnalysisResult : IAnalysisResult
    {
        public bool IsPassed { get; private set; } = false;
        public object? ReturnValue { get; private set; } = null;
        public Exception? Exception { get; private set; } = null;

        public GenericAnalysisResult() { }
        public GenericAnalysisResult(bool isPassed = false, object? returnValue = null, Exception? exception = null)
        {
            IsPassed = isPassed;
            ReturnValue = returnValue;
            Exception = exception;
        }
    }
}