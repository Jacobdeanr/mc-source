using System.Collections.Generic;
using System.Linq;

namespace McSource.Models.Config
{
  public class Textures
  {
    public IDictionary<string, Namespace> Namespaces { get; set; }

    public Textures(object root) : this((IDictionary<object, object>) root)
    {
    }

    public Textures(IDictionary<object, object> root)
      : this(root.ToDictionary(pair => pair.Key.ToString(), pair => new Namespace(pair.Value)))
    {
    }

    public Textures(IDictionary<string, Namespace> root)
    {
      Namespaces = root;
    }
  }
}