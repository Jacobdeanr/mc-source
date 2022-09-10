using System.Collections.Generic;
using System.Text.RegularExpressions;
using McSource.Logging;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  public abstract class Block : IVmfModelConvertible<Vmf.Solid>
  {
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

    protected Block(ISchematic parent, BlockInfo info, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      Parent = parent;
      Info = info;
      BlockEntity = blockEntity;
      Coordinates = coordinates;
      Dimensions = new Dimensions3D(1);
    }

    public static Block Create(ISchematic parent,
      BlockInfo blockInfo, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      // todo catch special blocks here

      if (blockInfo.Id.Equals("air"))
      {
        return new IgnoredBlock(parent, blockInfo, coordinates, blockEntity);
      }

      return new DefaultBlock(parent, blockInfo, coordinates, blockEntity);
    }

    public IDictionary<McPosition3D, Block?> GetNeighbors() =>
      new Dictionary<McPosition3D, Block?>(6)
      {
        [McPosition3D.East] = Parent.GetOrDefault(Coordinates.Clone().MoveX(-1)),
        [McPosition3D.West] = Parent.GetOrDefault(Coordinates.Clone().MoveX(1)),

        [McPosition3D.Bottom] = Parent.GetOrDefault(Coordinates.Clone().MoveY(-1)),
        [McPosition3D.Top] = Parent.GetOrDefault(Coordinates.Clone().MoveY(1)),

        [McPosition3D.South] = Parent.GetOrDefault(Coordinates.Clone().MoveZ(1)),
        [McPosition3D.North] = Parent.GetOrDefault(Coordinates.Clone().MoveZ(-1))
      };

    public abstract Solid? ToModel(IVmfRoot root);

    public override string ToString()
    {
      return $"{Coordinates}: '{Info}'";
    }
  }
}