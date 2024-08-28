using System;
using System.Diagnostics;
using System.Linq;
using System.IO;
using McSource.Core;
using McSource.Logging;
using McSource.Models.Nbt.Blocks.Abstract;

namespace McSource.Core.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputPath = null;
            string outputPath = null;
            string configPath = null;

            // Parse command line arguments
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                    case "--input":
                        if (i + 1 < args.Length)
                        {
                            inputPath = args[++i];
                        }
                        break;
                    case "-o":
                    case "--output":
                        if (i + 1 < args.Length)
                        {
                            outputPath = args[++i];
                        }
                        break;
                    case "-c":
                    case "--config":
                        if (i + 1 < args.Length)
                        {
                            configPath = args[++i];
                        }
                        break;
                }
            }

            // Validate required arguments
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Log.Critical("No input .schem file path provided. Use -i or --input to specify the path.");
                Console.WriteLine("Error: No input .schem file path provided. Use -i or --input to specify the path.");
                return;
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                Log.Critical("No output path provided. Use -o or --output to specify the path.");
                Console.WriteLine("Error: No output path provided. Use -o or --output to specify the path.");
                return;
            }

            if (string.IsNullOrWhiteSpace(configPath))
            {
                // Fallback to project root if no config path is provided
                var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.FullName;
                if (projectRoot == null)
                {
                    Log.Error("Could not determine project root directory.");
                    return;
                }
                configPath = Path.Combine(projectRoot, "config.yml");
            }

            Log.Info("Welcome to McSource. Starting up...");
            Log.Info($"Converting '{inputPath}' to '{outputPath}' using config '{configPath}'");

            var stopwatch = Stopwatch.StartNew();

            try
            {
                new Converter().Convert(inputPath, outputPath, configPath);
            }
            catch (Exception e)
            {
                Log.Critical("An Exception occurred. Stopping...", e);
            }

            Log.Info($"The conversion process took {(int)stopwatch.Elapsed.TotalMinutes:D2}min {stopwatch.Elapsed.Seconds:D2}s to complete.");

#if DEBUG
      foreach (var m in SolidBlock.MissingTextures.Distinct())
      {
        Log.Warning($"Texture file does not exist: '{m}'");
      }
#endif

            Console.WriteLine("Press any key to end the application...");
            Console.ReadKey();
        }
    }
}
