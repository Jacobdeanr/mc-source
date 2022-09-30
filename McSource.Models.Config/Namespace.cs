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
    {
      Blocks = root.ToDictionary(
        pair => pair.Key.ToString(),
        pair => new Block(this, pair.Key.ToString(), pair.Value));
    }
  }
}