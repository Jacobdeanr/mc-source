using McSource.Logging;

namespace McSource.Models.Config
{
  public class Side
  {
    public string? MaterialPath { get; set; }

    public Side(object root)
    {
      if (root is string materialPath)
      {
        MaterialPath = materialPath;
      }
    }
  }
}
