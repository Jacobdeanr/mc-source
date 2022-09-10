using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using McSource.Logging;
using McSource.Models.Config;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  public abstract class Block : IVmfModelConvertible<Vmf.Solid>, IEquatable<Block>
  {
    public Block? BlockGroupRoot { get; set; }

    protected Config.Block? Config { get; }

    /// <summary>
    /// Parent Schematic
    /// </summary>
    public ISchematic Parent { get; set; }

    /// <summary>
    /// Block size
    /// </summary>
    public Dimensions3D Dimensions { get; protected set; }

    /// <summary>
    /// Determines whether this block is see-through
    /// </summary>
    public bool Translucent { get; set; }

    /// <summary>
    /// Block Id
    /// </summary>
    public BlockInfo Info { get; set; }

    /// <summary>
    /// The block location in minecraft
    /// </summary>
    public Coordinates Coordinates { get; set; }

    /// <summary>
    /// Additional block data
    /// </summary>
    public BlockEntity? BlockEntity { get; set; }

    protected Block(ISchematic parent, BlockInfo info, Coordinates coordinates, BlockEntity? blockEntity = default,
      Config.Block? config = default)
    {
      Parent = parent;
      Info = info;
      BlockEntity = blockEntity;
      Coordinates = coordinates;
      Dimensions = new Dimensions3D(1);

      Config = config;
    }

    private static Config.Block? GetConfigEntry(Config.Config config, BlockInfo info)
    {
      if (!config.Textures.Namespaces.TryGetValue(info.Namespace, out var ns))
      {
        return default;
      }

      if (!ns.Blocks.TryGetValue(info.Id, out var block))
      {
        return default;
      }

      return block;
    }

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
          Log.Warning($"{nameof(BlockType)} '{config.Type}' is not implemented. Using {nameof(SolidBlock)} instead");
          break;
        case BlockType.Ignored:
          return new IgnoredBlock(parent, blockInfo, coordinates, blockEntity);
        case BlockType.Block:
          // Use fallback method return
          break;
        default:
          Log.Error($"Invalid {nameof(BlockType)} '{config?.Type}'. Using {nameof(SolidBlock)} instead");
          break;
      }

      return new SolidBlock(parent, blockInfo, coordinates, blockEntity);
    }

    public IDictionary<McDirection3D, Block?> GetNeighbors() =>
      new Dictionary<McDirection3D, Block?>(6)
      {
        [McDirection3D.East] = Parent.GetOrDefault(Coordinates.Clone().MoveX(-1)),
        [McDirection3D.West] = Parent.GetOrDefault(Coordinates.Clone().MoveX(1)),

        [McDirection3D.Bottom] = Parent.GetOrDefault(Coordinates.Clone().MoveY(-1)),
        [McDirection3D.Top] = Parent.GetOrDefault(Coordinates.Clone().MoveY(1)),

        [McDirection3D.South] = Parent.GetOrDefault(Coordinates.Clone().MoveZ(1)),
        [McDirection3D.North] = Parent.GetOrDefault(Coordinates.Clone().MoveZ(-1))
      };

    public abstract Solid? ToModel(IVmfRoot root);

    public override string ToString()
    {
      return $"{Coordinates}: '{Info}'";
    }

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

    public void Extend(Block block, McDirection3D direction)
    {
      switch (direction)
      {
        case McDirection3D.Top: 
          Dimensions.DX += block.Dimensions.DX;
          break;
        case McDirection3D.South:
          Dimensions.DZ += block.Dimensions.DZ;
          break;
        case McDirection3D.East:
          Dimensions.DY += block.Dimensions.DY;
          break;
        default:
          throw new ArgumentOutOfRangeException($"Extending a block in {nameof(direction)} '{direction}' is currently not supported");
      }
    }
  }
}