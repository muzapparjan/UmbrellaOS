using System.Diagnostics;

namespace UmbrellaOS.Generic.Extensions
{
    public static class ProcessExtensions
    {
        public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            while (!process.HasExited && !cancellationToken.IsCancellationRequested)
                await Task.Yield();
        }
    }
}