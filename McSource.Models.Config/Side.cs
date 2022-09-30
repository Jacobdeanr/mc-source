using McSource.Logging;

namespace McSource.Models.Config
{
  public class Side
  {
    public Namespace Namespace { get; set; }
    public string BlockId { get; set; }
    
    public string? MaterialPath { get; set; }

    public string? Get()
    {
      return MaterialPath;
    }
    
    public Side(Namespace ns, string blockId, object? root)
    {
      BlockId = blockId;
      Namespace = ns;

      if (root is string materialPath)
      {
        MaterialPath = materialPath;
      }
    }
  }
}
