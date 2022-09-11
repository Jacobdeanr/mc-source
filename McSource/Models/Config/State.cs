using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;

namespace McSource.Models.Config
{
  public class State : Stage
  {
    public IDictionary<string, Stage>? Stages { get; set; }

    public string? Get(string? stage, string? part = null, SidePosition? side = null)
    {
      var result = Get(part, side);
      if (result != null)
      {
        return result;
      }
      
      if (Stages != null && stage != null)
      {
        if (Stages.TryGetValue(stage, out var value))
        {
          return value?.Get(part, side);
        }
      }

      return Get(part, side);
    }

    public State(Namespace ns, string blockId, object root) : base(ns, blockId, root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("stages", out IDictionary<object, object> stages))
      {
        return;
      }

      if (Parts != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      Stages = stages.ToDictionary(pair => pair.Key.ToString(), pair => new Stage(ns, blockId, pair.Value));
    }
  }
}