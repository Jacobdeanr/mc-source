using System.IO;
using System.Linq;

namespace McSource.Models.Nbt.Structs
{
  public struct BlockId
  {
    public string Id { get; set; }
    public string Namespace { get; set; }
    
    public string Properties { get; set; }

    public BlockId(string id, string @namespace = "minecraft")
    {
      Id = id;
      Namespace = @namespace;
      Properties = null; // todo
    }

    public static BlockId FromString(string idString)
    {
      var result = new BlockId(idString);

      var split = idString.Split(":");
      if (split.Length <= 1)
      {
        return result;
      }

      result.Id = string.Join(":", split.Skip(1));
      result.Namespace = split[0];

      return result;
    }

    public string ToPath() => $"{Namespace}/{Id}";
    public override string ToString() => $"{Namespace}:{Id}";
  }
}