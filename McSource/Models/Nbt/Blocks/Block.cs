using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks.Components;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks
{
  public enum NeighborPosition
  {
    Top,
    Bottom,
    East,
    West,
    North,
    South
  }

  public abstract class Block : IVmfModelConvertible<Vmf.Solid>
  {
    public ISchematic Parent { get; set; }
    
    public Dimensions3D Dimensions { get; protected set; }

    /// <summary>
    /// Block Id
    /// </summary>
    public BlockId BlockId { get; set; }

    public Coordinates Coordinates { get; set; }

    /// <summary>
    /// Additional block data
    /// </summary>
    public BlockEntity? BlockEntity { get; set; }

    /// <summary>
    /// Block properties
    /// </summary>
    public ICollection<Property>? Properties { get; set; }

    public Texture Texture { get; }

    protected Block(ISchematic parent, BlockId blockId, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      Parent = parent;
      BlockId = blockId;
      BlockEntity = blockEntity;
      Coordinates = coordinates;
      Properties = ParseProperties(blockId.Properties);
      Dimensions = new Dimensions3D(Constants.BlockSize);
      Texture = new Texture(this, BlockId.ToPath());
    }

    protected static ICollection<Property> ParseProperties(string idString)
    {
      var regex = new Regex(@"\[([^]]+)\]", RegexOptions.Compiled);
      // todo parse metadata in '[]'
      return default;
    }

    public static Block? Create(ISchematic parent, BlockId blockId, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      // todo catch special blocks here

      if (blockId.Id.Equals("air"))
      {
        return null;
      }

      return new DefaultBlock(parent, blockId, coordinates, blockEntity);
    }

    public override string ToString()
    {
      return $"{Coordinates}: '{BlockId}'";
    }

    public bool IsEncased => Parent.GetNeighbors(this).Count == 6;

    public abstract Vmf.Solid? ToModel(IVmfRoot root);
  }
}