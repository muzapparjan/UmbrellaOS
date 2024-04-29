using UmbrellaOS.Tests.Generic;
using UmbrellaOS.Tests.Generic.Interfaces;

namespace UmbrellaOS.Tests.T000002;

public sealed class TestEncodeInstructions : ITest
{
    public async Task<ITestResult> RunAsync(CancellationToken cancellationToken = default, params object[] parameters)
    {
        try
        {
            return await TestAllInstructions(cancellationToken, parameters);
        }
        catch (Exception exception)
        {
            return new GenericTestResult(false, null, exception);
        }
    }
    private async Task<ITestResult> TestAllInstructions(CancellationToken cancellationToken = default, params object[] parameters)
    {
        await TestIntel.TestAll(cancellationToken, parameters);
        return new GenericTestResult(true, null, null);
    }
}