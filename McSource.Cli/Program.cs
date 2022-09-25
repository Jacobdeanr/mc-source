using System;
using System.Diagnostics;
using System.Linq;
using McSource.Logging;
using McSource.Models.Nbt.Blocks.Abstract;

namespace McSource.Cli
{
  /*
   * Milestones:
   * - todo more block behaviors
   */

  public class Program
  {
    public static void Main(string[] args)
    {
      // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\dual.schem";
      // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\axis.schem";
      var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\ttt_kekland.schem";
      // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\cobble_glowstone.schem";
      // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\cobble_glowstone_air.schem";

      Log.Info("Welcome to McSource. Starting up...");
      Log.Info($"Converting '{path}'");

      var stopwatch = Stopwatch.StartNew();

      try
      {
        new Converter().Convert(path, @"C:\Users\Simon\Documents\Userdata\SourceCraft\mc-source-out\map.vmf");
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