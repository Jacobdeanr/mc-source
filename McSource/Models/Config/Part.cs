using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Logging;

namespace McSource.Models.Config
{
  public class Part : Side
  {
    public IDictionary<string, Side>? Sides { get; set; }

    public Part([NotNull] object root) : base(root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("sides", out IDictionary<object, object> sides))
      {
        return;
      }

      if (MaterialPath != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      Sides = sides.ToDictionary(pair => pair.Key.ToString(), pair => new Side(pair.Value));
    }
  }
}