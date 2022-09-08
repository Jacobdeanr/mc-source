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

      try
      {
        var nbtFile = new NbtFile();
        nbtFile.LoadFromFile(schematicPath);
        schematic = SpongeSchematic.FromTag(nbtFile.RootTag);
      }
      catch (Exception e)
      {
        Log.Error("Could not read schematic file", e);
      }

      if (schematic == null)
      {
        return;
      }

      try
      {
        schematic.ToModel().ToFile(outputPath);
      }
      catch (Exception e)
      {
        Log.Error("Could not serialize map to file", e);
      }
      
      Log.Info("Successfully created vmf file");
    }
  }
}