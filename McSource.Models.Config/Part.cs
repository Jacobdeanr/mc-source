using System;
using System.Collections.Generic;
using McSource.Common.Extensions;

namespace McSource.Models.Config
{
  public class Part : Side
  {
    public Dictionary<Enums.SidePosition, Side?>? Sides { get; set; }

    public string? Get(Enums.SidePosition? side)
    {
      if (Sides != null && side != null)
      {
        if (Sides.TryGetValue(side.Value, out var value))
        {
          return value?.Get();
        }
      }

      return Get();
    }

    public Part(Namespace ns, string blockId, object root) : base(ns, blockId, root)
    {
      if (!(root is IDictionary<object, object> rootDict) || !rootDict.TryGetValue("sides", out IDictionary<object, object?> sides))
      {
        return;
      }

      if (MaterialPath != null)
      {
        throw new Exception("A texture cannot contain more than one texture definition");
      }
      
      var textureDefault = sides.GetOrDefault("default") ?? null;
      var textureAll =  sides.GetOrDefault("all") ?? textureDefault;
      var textureSides =  sides.GetOrDefault("side") ?? textureAll;

      var top = sides.GetOrDefault("top") ?? textureSides;
      var bottom = sides.GetOrDefault("bottom") ?? textureSides;

      Sides = new Dictionary<Enums.SidePosition, Side?>
      {
        {Enums.SidePosition.Top,  new Side(ns, blockId, top ?? bottom)},
        {Enums.SidePosition.Bottom, new Side(ns, blockId, bottom ?? top ?? textureSides)},
        {Enums.SidePosition.Front, new Side(ns, blockId, sides.GetOrDefault("front") ?? textureSides)},
        {Enums.SidePosition.Back, new Side(ns, blockId, sides.GetOrDefault("back") ?? textureSides)},
        {Enums.SidePosition.Right, new Side(ns, blockId, sides.GetOrDefault("right") ?? textureSides)},
        {Enums.SidePosition.Left, new Side(ns, blockId, sides.GetOrDefault("left") ?? textureSides)},
      };
      
      
      // if (textures.All(t => t == null))
      // {
      //   // No sides specified
      //   return;
      // }
    }
  }
}