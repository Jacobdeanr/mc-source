using System.Collections.Generic;
using McSource.Models.Nbt.Blocks;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using VmfSharp;

namespace McSource.Models.Nbt.Schematic
{
  public interface ISchematic
  {
    public Config.Config Config { get; }
    public Coordinates Offset { get; }

    public Dimensions3D Dimensions { get; }

    public Block[,,] Blocks { get; }

    public Vmf.Map ToModel();

    public void Add(Block block, Coordinates c);
    public void Add(Block block, short x, short y, short z);
    public Block? GetOrDefault(Coordinates c);
    public Block? GetOrDefault(short x, short y, short z);
    public Block Get(Coordinates c);
    public Block Get(short x, short y, short z);
    public bool TryGet(Coordinates c, out Block? block);
    public bool TryGet(short x, short y, short z, out Block? block);
  }
}