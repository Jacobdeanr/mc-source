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
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\axis.schem";
        var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\ttt_kekland.schem";
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\cobble_glowstone.schem";
        // var path = @"C:\Users\Simon\Documents\Userdata\SourceCraft\cobble_glowstone_air.schem";
        
        Log.Info("Welcome to Converter. Starting up...");
        new Converter().Convert(path, @"C:\Users\Simon\Documents\Userdata\SourceCraft\map.vmf");
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