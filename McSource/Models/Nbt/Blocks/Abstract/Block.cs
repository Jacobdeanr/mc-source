using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Logging;
using McSource.Models.Config;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using Side = McSource.Models.Config.Side;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  /// <summary>
  /// Minecraft Block data model
  /// </summary>
  public abstract class Block : IEquatable<Block>
  {
    #region Properties

    public McDirection3D Facing { get; private set; } = McDirection3D.North;
    public string State { get; private set; } = "default";
    public string Part { get; private set; } = "default";
    public string Stage { get; private set; } = "default";

#if DEBUG
    public static ICollection<string> MissingTextures = new List<string>();
#endif

    /// <summary>
    /// Reference to the parent block group this instance is attached to. Null if not part of a block group.
    /// </summary>
    public BlockGroup? ParentBlockGroup { get; set; }

    /// <summary>
    /// Indicates whether this block should be drawn
    /// </summary>
    public virtual bool CanDraw { get; protected set; }

    /// <summary>
    /// Block-specific configuration
    /// </summary>
    protected Config.Block? Config { get; }

    /// <summary>
    /// Parent Schematic
    /// </summary>
    public ISchematic Parent { get; }

    /// <summary>
    /// Block size
    /// </summary>
    public Dimensions3D Dimensions { get; protected set; }

    /// <summary>
    /// Determines whether this block is see-through
    /// </summary>
    public bool Translucent { get; protected set; }

    /// <summary>
    /// Block Id
    /// </summary>
    public BlockInfo Info { get; }

    /// <summary>
    /// The block location in minecraft
    /// </summary>
    public Coordinates Coordinates { get; }

    /// <summary>
    /// Additional block data
    /// </summary>
    public BlockEntity? BlockEntity { get; }

    #endregion

    protected Block(
      ISchematic parent,
      BlockInfo info,
      Coordinates coordinates,
      Config.Block? config,
      BlockEntity? blockEntity = default)
    {
      Parent = parent;
      Info = info;
      BlockEntity = blockEntity;
      Coordinates = coordinates;
      Dimensions = new Dimensions3D(1);

      Config = config;
      if (config != null)
      {
        Translucent = config.Translucent;
      }
    }

    public virtual void Prepare()
    {
      CanDraw = GetNeighbors().Values.Count(neighbor => neighbor.IsOpaque()) != 6;
    }

    public void Extend(int amount, McDirection3D direction)
    {
      switch (direction)
      {
        case McDirection3D.East:
        case McDirection3D.West:
          Dimensions.DY += (short) (Dimensions.DY * amount);
          break;
        case McDirection3D.North:
        case McDirection3D.South:
          Dimensions.DZ += (short) (Dimensions.DZ * amount);
          break;
        case McDirection3D.Top:
        case McDirection3D.Bottom:
          Dimensions.DX += (short) (Dimensions.DX * amount); // do not change
          break;
      }
    }

    private static Config.Block? GetConfigEntry(Config.Config config, BlockInfo info)
    {
      if (!config.Textures.Namespaces.TryGetValue(info.Namespace, out var ns))
      {
        return default;
      }

      return !ns.Blocks.TryGetValue(info.Id, out var block)
        ? default
        : block;
    }

    public IDictionary<McDirection3D, Block?> GetNeighbors() =>
      new Dictionary<McDirection3D, Block?>(6)
      {
        [McDirection3D.East] = Parent.GetOrDefault(Coordinates.X - 1, Coordinates.Y, Coordinates.Z),
        [McDirection3D.West] = Parent.GetOrDefault(Coordinates.X + 1, Coordinates.Y, Coordinates.Z),

        [McDirection3D.Bottom] = Parent.GetOrDefault(Coordinates.X, Coordinates.Y - 1, Coordinates.Z),
        [McDirection3D.Top] = Parent.GetOrDefault(Coordinates.X, Coordinates.Y + 1, Coordinates.Z),

        [McDirection3D.South] = Parent.GetOrDefault(Coordinates.X, Coordinates.Y, Coordinates.Z + 1),
        [McDirection3D.North] = Parent.GetOrDefault(Coordinates.X, Coordinates.Y, Coordinates.Z - 1)
      };

    protected string GetTexturePath(McDirection3D facePosition)
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


    protected virtual Face.Face GetFace(McDirection3D pos)
    {
      var path = GetTexturePath(pos);

#if DEBUG
      var fullPath = @"C:\Program Files (x86)\Steam\steamapps\common\GarrysMod\garrysmod\materials\" + path.Replace("/", "\\");
      if (!File.Exists(fullPath + ".vmt") || !File.Exists(fullPath + ".vtf"))
      {
        MissingTextures.Add(path);
      }
#endif

      return new SolidFace(this, pos, path);
    }

    #region Static Methods

    [SuppressMessage("ReSharper", "RedundantCaseLabel")]
    public static Block Create(ISchematic parent,
      BlockInfo blockInfo, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      // todo: Catch special blocks here

      var config = GetConfigEntry(parent.Config, blockInfo);
      switch (config?.Type)
      {
        case BlockType.Door:
        case BlockType.Fence:
        case BlockType.FenceGate:
        case BlockType.Fire:
        case BlockType.Flat:
        case BlockType.Ladder:
        case BlockType.Pane:
        case BlockType.Plant:
        case BlockType.Rod:
        case BlockType.Sign:
        case BlockType.Slab:
        case BlockType.Stairs:
        case BlockType.Torch:
        case BlockType.Trapdoor:
          Log.Warning($"{nameof(BlockType)} '{config.Type}' has no specific implementation. Using {nameof(DetailBlock)} instead");
          return new DetailBlock(parent, blockInfo, coordinates, config, blockEntity);
        case BlockType.Ignored:
          return new IgnoredBlock(parent, blockInfo, coordinates, blockEntity);
        default:
        case BlockType.Block:
          // Use method return as fallback
          break;
      }

      return new SolidBlock(parent, blockInfo, coordinates, config, blockEntity);
    }

    protected virtual IEnumerable<Vmf.Side> GetSolidSides(Vmf.Solid solid)
    {
      return Enum.GetValues(typeof(McDirection3D))
        .Cast<McDirection3D>()
        .Select(d => GetFace(d).ToModel(solid));
    }

    protected Vmf.Solid GetSolid(IVmfRoot root)
    {
      var solid = new Vmf.Solid(root);
      solid.Sides = GetSolidSides(solid).ToArray();
      solid.Editor = Editor.Default(solid);
      return solid;
    }

    #endregion

    #region Serialization

    public override string ToString() => $"{Coordinates}: '{Info}'";

    #endregion

    #region IEquatable

    public bool Equals(Block? other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return Info.Equals(other.Info) && Equals(BlockEntity, other.BlockEntity);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != this.GetType())
      {
        return false;
      }

      return Equals((Block) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Info, BlockEntity);
    }

    #endregion
  }
}