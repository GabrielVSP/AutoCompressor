using System;
using System.IO;
using System.Text.Json;
using Avalonia.Platform;

namespace AutoCompressor
{
    public class AppConfig
    {
        public string inputFolder { get; set; }
        public string outputFolder { get; set; }

        private static string ConfigFilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AutoCompressor", "config.json");

        public static AppConfig Load()
        {
            if (File.Exists(ConfigFilePath))
            {
                string json = File.ReadAllText(ConfigFilePath);
                return JsonSerializer.Deserialize<AppConfig>(json);
            }

            return new AppConfig();
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

            var directory = Path.GetDirectoryName(ConfigFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
