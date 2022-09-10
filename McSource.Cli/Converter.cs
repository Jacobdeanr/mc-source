using System;
using System.Collections;
using System.Reflection.Metadata;
using fNbt;
using McSource.Logging;
using McSource.Models;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Cli
{
  public class Converter
  {
    public void Convert(string schematicPath, string outputPath)
    {
      ISchematic? schematic = null;
      
      var config = Models.Config.Config.FromFile(@"C:\Users\Simon\mc-source\mc-source\config.yml");
      if (config == null)
      {
        Log.Error("Could not load config");
        return;
      }
      
      try
      {
        var nbtFile = new NbtFile();
        nbtFile.LoadFromFile(schematicPath);
        schematic = SpongeSchematic.FromTag(nbtFile.RootTag, config);
      }
      catch (Exception e)
      {
        Log.Error("Could not read schematic file", e);
        return;
      }

      if (schematic == null)
      {
        Log.Error("Could not read schematic file");
        return;
      }

      try
      {
        schematic.ToModel().ToFile(outputPath);
      }
      catch (Exception e)
      {
        Log.Error("Could not serialize map to file", e);
        return;
      }
      
      Log.Info($"Successfully created vmf file '{outputPath}'");
    }
  }
}