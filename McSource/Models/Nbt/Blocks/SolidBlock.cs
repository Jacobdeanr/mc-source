using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Logging;
using McSource.Models.Config;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using Block = McSource.Models.Config.Block;

namespace McSource.Models.Nbt.Blocks
{
  /// <summary>
  /// Default solid, non-translucent minecraft block without any special attributes
  /// </summary>
  public class SolidBlock : TexturedBlock
  {
    public McDirection3D Facing { get; set; } = McDirection3D.North;
    public string State { get; set; } = "default";
    public string Part { get; set; } = "default";
    public string Stage { get; set; } = "default";

    private string GetTexturePath(McDirection3D facePosition)
    {
      string? path = null;

      if (Config?.Texture != null)
      {
        path = Facing switch
        {
          McDirection3D.East => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            _ => path
          },
          McDirection3D.West => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            _ => path
          },
          McDirection3D.North => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            _ => path
          },
          McDirection3D.South => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            _ => path
          },
          McDirection3D.Top => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            _ => path
          },
          McDirection3D.Bottom => facePosition switch
          {
            McDirection3D.East => Config.Texture.Get(Stage, State, Part, SidePosition.Left),
            McDirection3D.West => Config.Texture.Get(Stage, State, Part, SidePosition.Right),
            McDirection3D.North => Config.Texture.Get(Stage, State, Part, SidePosition.Top),
            McDirection3D.South => Config.Texture.Get(Stage, State, Part, SidePosition.Bottom),
            McDirection3D.Top => Config.Texture.Get(Stage, State, Part, SidePosition.Back),
            McDirection3D.Bottom => Config.Texture.Get(Stage, State, Part, SidePosition.Front),
            _ => path
          },
          _ => path
        };
      }

      return path == null
        ? Info.ToPath()
        : $"{Info.Namespace}/{path}";
    }

#if DEBUG
    public static ICollection<string> Missing = new List<string>();
#endif
    
    protected override Face.Face GetFace(McDirection3D pos)
    {
      var path = GetTexturePath(pos);

#if DEBUG
      var fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\GarrysMod\garrysmod\materials\" + path.Replace("/", "\\");
      if (!File.Exists(fullPath + ".vmt") || !File.Exists(fullPath + ".vtf"))
      {
        Missing.Add(path);
      }
#endif

      return new SolidFace(this, pos, path);
    }

    public SolidBlock([NotNull] ISchematic parent, [NotNull] BlockInfo info, Coordinates coordinates, [CanBeNull] Block? config,
      [CanBeNull] BlockEntity? blockEntity = default) : base(parent, info, coordinates, config, blockEntity)
    {
    }
  }
}