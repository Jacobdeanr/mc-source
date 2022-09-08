using System.Collections.Generic;
using McSource.Models.Nbt.Blocks;
using VmfSharp;

namespace McSource.Models.Nbt.Schematic
{
  public interface ISchematic
  {
    public string Name { get; }

    public Coordinates Offset { get; }

    public Dimensions3D Dimensions { get; }

    public ICollection<Block> Blocks { get; }

    public Vmf.Map ToModel();
  }
}