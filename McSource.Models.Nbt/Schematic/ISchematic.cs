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
    public Block[,,] Blocks { get; }
    public Coordinates Offset { get; }
    public Dimensions3D Dimensions { get; }

    public void Add(Block block, short x, short y, short z);
    public Block? GetOrDefault(short x, short y, short z);
    public Block Get(short x, short y, short z);
    public bool TryGet(short x, short y, short z, out Block? block);
    
    public void OnLoaded();

    public Vmf.Map ToModel();
  }
}