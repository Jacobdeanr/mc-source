using System;
using System.Collections.Generic;
using System.Linq;
using McSource.Common.Extensions;

namespace McSource.Models.Config
{
  public class Stage : Part
  {
    public IDictionary<string, Part>? Parts { get; set; }

    
    public string? Get(string? part, Enums.SidePosition? side = null)
    {
      var result = Get(side);
      if (result != null)
      {
        return result;
      }
      
      if (Parts != null && part != null)
      {
        if (Parts.TryGetValue(part, out var value))
        {
          return value?.Get(side);
        }
      }

      return Get(side);
    }
    
    public Stage(Namespace ns, string blockId, object root) : base(ns, blockId, root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("parts", out IDictionary<object, object> parts))
      {
        return;
      }

      if (Sides != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      Parts = parts.ToDictionary(pair => pair.Key.ToString(), pair => new Part(ns, blockId, pair.Value));
    }
  }
}