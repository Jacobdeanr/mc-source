using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks.Components;
using McSource.Models.Nbt.Properties;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks
{
  public abstract class Block : IVmfModelConvertible<Vmf.Solid>
  {
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

    protected Block(string idString, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      BlockEntity = blockEntity;
      Coordinates = coordinates;
      BlockId = BlockId.FromString(idString);
      Properties = ParseProperties(idString);
      Dimensions = new Dimensions3D(Constants.BlockSize);
      Texture = new Texture(this, BlockId.ToPath());
    }

    protected static ICollection<Property> ParseProperties(string idString)
    {
      var regex = new Regex(@"\[([^]]+)\]", RegexOptions.Compiled);
      // todo parse metadata in '[]'
      return default;
    }

    public static Block FromString(string blockString, Coordinates coordinates, BlockEntity? blockEntity = default)
    {
      // todo catch special blocks here

      return new DefaultBlock(blockString, coordinates, blockEntity);
    }

    public override string ToString()
    {
      return $"{Coordinates}: '{BlockId}'";
    }

    public abstract Vmf.Solid ToModel(IVmfRoot root);
  }
}