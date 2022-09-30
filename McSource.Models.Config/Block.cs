using System;
using System.Collections.Generic;
using McSource.Logging;

namespace McSource.Models.Config
{
  public class Block
  {
    public Namespace Namespace { get; set; }

    public string BlockId { get; set; }

    public Enums.BlockType Type { get; set; } = Enums.BlockType.Block;
    public bool Translucent { get; set; }
    public Texture Texture { get; set; }

    public Block(Namespace ns, string blockId, object root) : this(ns, blockId, (IDictionary<object, object>) root)
    {
    }

    public Block(Namespace ns, string blockId, IDictionary<object, object> root)
    {
      Namespace = ns;
      BlockId = blockId;
      
      if (root.TryGetValue("type", out var typeValue))
      {
        if (Enum.TryParse<Enums.BlockType>(typeValue.ToString(), true, out var type))
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
          Log.Error($"Could not parse 'translucent: {translucentValue}' for {nameof(Block)}");
        }
      }

      if (root.TryGetValue("texture", out var textureValue))
      {
        Texture = new Texture(Namespace, BlockId, textureValue);
      }
    }
  }
}