using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using McSource.Extensions;
using McSource.Logging;
using McSource.Models.Config;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using Side = McSource.Models.Config.Side;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  public abstract class Block : IVmfModelConvertible<Vmf.Solid>, IEquatable<Block>
  {
    public virtual bool CanDraw => !IsEncased;
    
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

    protected Block(ISchematic parent,
      BlockInfo info, Coordinates coordinates, Config.Block? config, BlockEntity? blockEntity = default)
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

    public bool IsEncased { get; set; }

    public virtual void Prepare()
    {
      int count = 0;
      foreach (var neighbor in GetNeighbors().Values)
      {
        if (neighbor?.Translucent == false)
        {
          if (neighbor.Info.Id.Contains("water"))
          {
            
          }
          count++;
        }
      }

      IsEncased =  count == 6;
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
          Log.Warning($"{nameof(BlockType)} '{config.Type}' is not implemented. Using {nameof(SolidBlock)} instead");
          break;
        case BlockType.Ignored:
          return new IgnoredBlock(parent, blockInfo, coordinates, blockEntity);
        default:
        case BlockType.Block:
          // Use fallback method return
          break;
      }

      return new SolidBlock(parent, blockInfo, coordinates, config, blockEntity);
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

    public BlockGroup? ParentBlockGroup { get; set; }

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
  }
}