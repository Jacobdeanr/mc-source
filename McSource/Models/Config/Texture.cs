using System;
using System.Collections.Generic;
using System.Linq;
using McSource.Logging;
using McSource.Extensions;

namespace McSource.Models.Config
{
  public class Texture : State
  {
    public IDictionary<string, State>? States { get; set; }

    public string? Get(string state, string? stage = null, string? part = null, SidePosition? side = null)
    {
      var result = Get(stage, part, side);
      if (result != null)
      {
        return result;
      }
      
      if (States != null)
      {
        if (States.TryGetValue(state, out var value))
        {
          return value?.Get(stage, part, side);
        }
      }

      return Get(stage, part, side);
    }

    public Texture(Namespace ns, string blockId, object root) : base(ns, blockId, root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("states", out IDictionary<object, object> stages))
      {
        return;
      }

      if (Stages != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      States = stages.ToDictionary(pair => pair.Key.ToString(), pair => new State(ns, blockId, pair.Value));
    }
  }
}