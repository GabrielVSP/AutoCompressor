using System;
using System.IO;
using System.Text.Json;

namespace AutoCompressor
{
    internal class AppConfig
    {
        public string InputFolder { get; set; }
        public string OutputFolder { get; set; }

        private static string ConfigFilePath
        {
            get
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var appFolder = Path.Combine(appDataPath, "AutoCompressor");

                if (!Directory.Exists(appFolder))
                {
                    Directory.CreateDirectory(appFolder);
                }

                return Path.Combine(appFolder, "config.json");
            }
        }

        public static AppConfig Load()
        {
            if (File.Exists(ConfigFilePath))
            {
                string json = File.ReadAllText(ConfigFilePath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }

            return new AppConfig();
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
