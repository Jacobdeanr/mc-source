using System;
using System.Collections.Generic;
using McSource.Logging;

namespace McSource.Models.Config
{
  public class Block
  {
    public BlockType Type { get; set; } = BlockType.Block;
    public bool Translucent { get; set; }
    public Texture Texture { get; set; }

    public Block(object root) : this((IDictionary<object, object>) root)
    {
    }
    
    public Block(IDictionary<object, object> root)
    {
      if (root.TryGetValue("type", out var typeValue))
      {
        if (Enum.TryParse<BlockType>(typeValue.ToString(), true, out var type))
        {
          Type = type;
        }
        else
        {
          Log.Error($"Could not parse 'type' value '{typeValue}' for {nameof(Block)}");
        }
      }

      if (root.TryGetValue("translucent", out var translucentValue))
      {
        if (bool.TryParse(translucentValue.ToString(), out var translucent))
        {
          Translucent = translucent;
        }
        else
        {
          Log.Error($"Could not parse 'translucent' value for {nameof(Block)}");
        }
      }

      if (root.TryGetValue("texture", out var textureValue))
      {
        Texture = new Texture(textureValue);
      }
    }
  }
}