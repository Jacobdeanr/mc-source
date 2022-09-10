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

    public State([NotNull] object root) : base(root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("stages", out IDictionary<object, object> stages))
      {
        return;
      }

      if (MaterialPath != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      Stages = stages.ToDictionary(pair => pair.Key.ToString(), pair => new Stage(pair.Value));
    }
  }
}