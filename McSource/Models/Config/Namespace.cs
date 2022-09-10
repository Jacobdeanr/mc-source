using System.Collections.Generic;
using System.Linq;

namespace McSource.Models.Config
{
  public class Namespace
  {
    public IDictionary<string, Block> Blocks { get; set; }

    public Namespace(object root) : this((IDictionary<object, object>) root)
    {
      
    }
    public Namespace(IDictionary<object, object> root)
      : this(root.ToDictionary(pair => pair.Key.ToString(), pair => new Block(pair.Value)))
    {
      
    }
    
    public Namespace(IDictionary<string, Block> root)
    {
      Blocks = root;
    }
  }
}