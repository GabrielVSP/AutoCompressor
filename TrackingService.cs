using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AutoCompressor
{
    public class TrackingService : BackgroundService
    {
        private readonly string folderToTrack = @"C:\videos";
        private FileSystemWatcher? watcher;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            if (!Directory.Exists(folderToTrack))
                Directory.CreateDirectory(folderToTrack);

            watcher = new FileSystemWatcher(folderToTrack)
            {
                NotifyFilter = NotifyFilters.FileName,
                Filter = "*.mp4"
            };

            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Tracking folder: {folderToTrack}");
            return Task.CompletedTask;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"New video added: {e.FullPath}");
            Compressor.Compress(@$"{e.FullPath}", $@"C:\Users\gabri\Videos\Compressed\compressed_{Path.GetFileName(e.FullPath)}");
        }

        public override void Dispose()
        {
            watcher?.Dispose();
            base.Dispose();
        }
    }
}