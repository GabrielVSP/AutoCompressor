using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AutoCompressor
{
    public class TrackingService : BackgroundService
    {
        
        private readonly string folderToTrack;
        private readonly string outputFolder;
        private FileSystemWatcher? watcher;

        public TrackingService(IOptions<AppConfig> config)
        {
            var c = AppConfig.Load();
            folderToTrack = c.inputFolder;
            outputFolder = c.outputFolder;
        }

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

            Logs.AddLog($"Tracking folder: {folderToTrack}");
            return Task.CompletedTask;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Logs.AddLog($"New video added: {e.FullPath}");
            Compressor.Compress(@$"{e.FullPath}", $@"{outputFolder}\compressed_{Path.GetFileName(e.FullPath)}");
        }

        public override void Dispose()
        {
            watcher?.Dispose();
            base.Dispose();
        }
    }
}