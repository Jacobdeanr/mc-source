using System.Collections.Generic;
using McSource.Models.Nbt.Blocks;
using McSource.Models.Vmf;

namespace McSource.Models.Nbt.Schematic
{
  public abstract class Schematic<T> : ISchematic
  {
    public string Name { get; set; }

    protected Schematic(string name)
    {
      Name = name;
    }

    public Coordinates Offset { get; protected set; } = new Coordinates(0, 0, 0);

    public Dimensions3D Dimensions { get; protected set; } = new Dimensions3D();

    public ICollection<Block> Blocks { get; protected set; } = new List<Block>();
    
    public abstract Map ToModel();

    public abstract ISchematic Load(T src);
  }
}