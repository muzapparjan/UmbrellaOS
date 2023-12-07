namespace UmbrellaOS.Sandbox
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var imageStream = File.OpenWrite("umbrella_v0.0.1.iso");
            Umbrella.Build(imageStream);
            imageStream.Close();
        }
    }
}