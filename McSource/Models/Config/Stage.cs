using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;

namespace McSource.Models.Config
{
  public class Stage : Part
  {
    public IDictionary<string, Part>? Parts { get; set; }

    public Stage([NotNull] object root) : base(root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("parts", out IDictionary<object, object> parts))
      {
        return;
      }

      if (MaterialPath != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }

      Parts = parts.ToDictionary(pair => pair.Key.ToString(), pair => new Part(pair.Value));
    }
  }
}