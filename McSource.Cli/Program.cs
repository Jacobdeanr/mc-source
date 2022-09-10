using System;
using McSource.Logging;

namespace McSource.Cli
{
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\dual.schem";
        var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\axis.schem";
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\ttt_kekland.schem";
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\cobble_glowstone.schem";
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\schematics\cobble_glowstone_air.schem";
        
        Log.Info("Welcome to Converter. Starting up...");
        new Converter().Convert(path, @"C:\Users\Simon\Documents\Userdata\SourceCraft\mc-source-out\map.vmf");
      }
      catch (Exception e)
      {
        Log.Critical("An Exception occurred. Stopping...", e);
      }

      Console.WriteLine("Press any key to end the application...");
      Console.ReadKey();
    }
  }
}