using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AutoCompressor
{
    class Program
    {
        [STAThread]
        public static async Task Main(string[] args)
        {
            //AttachConsole();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<TrackingService>();
                })
                .Build();

            var avaloniaTask = Task.Run(() =>
            {
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            });

            await host.RunAsync();
            await avaloniaTask;
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private static void AttachConsole()
        {
            if (!AllocConsole())
            {
                Console.WriteLine("Operation fail.");
            }
        }
    }
}
