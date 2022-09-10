using System;
using System.Collections.Generic;
using System.IO;
using McSource.Logging;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace McSource.Models.Config
{
  public class Config
  {
    public string MaterialsPath { get; set; }
    public Textures Textures { get; set; }

    public Config(IDictionary<object, object> root)
    {
      MaterialsPath = root["materialsPath"].ToString();
      Textures = new Textures(root["textures"]);
    }

    public static Config? FromStream(TextReader stream)
    {
      var deserializer = new DeserializerBuilder().Build();

      try
      {
        var config = deserializer.Deserialize<IDictionary<object, object>>(stream);
        return new Config(config);
      }
      catch (SemanticErrorException e)
      {
        Log.Error($"Could not read config: Error at {e.Start}", e);
      }
      catch (Exception e)
      {
        Log.Error("Could not read config", e);
      }

      return default;
    }

    public static Config? FromFile(string filePath)
    {
      try
      {
        using StreamReader reader = File.OpenText(filePath);
        return Config.FromStream(reader);
      }
      catch (Exception e)
      {
        Log.Error("Could not read config", e);
      }

      return default;
    }
  }
}