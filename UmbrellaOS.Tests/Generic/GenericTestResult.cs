using UmbrellaOS.Tests.Generic.Interfaces;

namespace UmbrellaOS.Tests.Generic
{
    public sealed class GenericTestResult : ITestResult
    {
        public bool IsPassed { get; private set; } = false;
        public object? ReturnValue { get; private set; } = null;
        public Exception? Exception { get; private set; } = null;

        public GenericTestResult() { }
        public GenericTestResult(bool isPassed = false, object? returnValue = null, Exception? exception = null)
        {
            IsPassed = isPassed;
            ReturnValue = returnValue;
            Exception = exception;
        }
    }
}