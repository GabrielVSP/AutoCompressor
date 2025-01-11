using System;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace AutoCompressor
{
    public class Compressor
    {

        static public void Compress(string inputPath, string outputPath)
        {
            
            var inputFile = new MediaFile { Filename = inputPath };
            var outputFile = new MediaFile { Filename = outputPath };
            
            try
            {

                Logs.AddLog("Started compressing");

                using (var engine = new Engine())
                {

                    engine.GetMetadata(inputFile);

                    var options = new ConversionOptions
                    {
                        VideoBitRate = 1000
                    };

                    engine.Convert(inputFile, outputFile, options);

                }

                Logs.AddLog("Video has been compressed.");

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }

        }

    }
}