using CefSharp.OffScreen;

namespace UmbrellaLegacyBrain.Sandbox
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var task = Task.Run(RunBrowserAsync);
            task.Wait();
        }

        private static async Task RunBrowserAsync()
        {
            var browser = new ChromiumWebBrowser("www.baidu.com");
            var captureCount = 0;
            var captureDirectory = "Captures";
            if (Directory.Exists(captureDirectory))
                Directory.Delete(captureDirectory, true);
            Directory.CreateDirectory(captureDirectory);
            browser.FrameLoadEnd += async (sender, args) =>
            {
                Console.WriteLine($"[{args.Url}] [{args.HttpStatusCode}]");
                var data = await browser.CaptureScreenshotAsync(CefSharp.DevTools.Page.CaptureScreenshotFormat.Png);
                var path = $"{captureDirectory}/{Interlocked.Increment(ref captureCount)}.png";
                File.WriteAllBytes(path, data);
            };

            string[] urls = [
                "https://www.baidu.com/",
                "https://www.google.com.hk/",
                "https://cn.bing.com/",
                "https://github.com/"
            ];
            foreach (var url in urls)
                await browser.LoadUrlAsync(url);
            browser.Dispose();
        }
    }
}