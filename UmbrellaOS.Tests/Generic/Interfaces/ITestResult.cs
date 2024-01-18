namespace UmbrellaOS.Tests.Generic.Interfaces
{
    public interface ITestResult
    {
        public bool IsPassed { get; }
        public object? ReturnValue { get; }
        public Exception? Exception { get; }
    }
}