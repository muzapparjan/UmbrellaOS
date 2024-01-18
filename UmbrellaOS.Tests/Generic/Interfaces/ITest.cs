namespace UmbrellaOS.Tests.Generic.Interfaces
{
    /**
     * <summary>A task to test some parts of the Umbrella.</summary>
     */
    public interface ITest
    {
        public Task<ITestResult> RunAsync(CancellationToken cancellationToken = default, params object[] parameters);
    }
}