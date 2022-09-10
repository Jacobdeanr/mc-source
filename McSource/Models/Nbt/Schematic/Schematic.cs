using System.Collections.Generic;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Vmf;

namespace McSource.Models.Nbt.Schematic
{
  public abstract class Schematic<T> : ISchematic
  {
    public Config.Config Config { get; set; }
    
    public Schematic(Config.Config config)
    {
      Config = config;
    }
    
    public Coordinates Offset { get; protected set; } = new Coordinates(0, 0, 0);

    public Dimensions3D Dimensions { get; protected set; } = new Dimensions3D();

    public Block[,,] Blocks { get; protected set; }

    public abstract Map ToModel();

    public abstract ISchematic Load(T src);

    public void Add(Block block, Coordinates c)
    {
      Add(block, (short) c.X, (short) c.Y, (short) c.Z);
    }

    public void Add(Block block, short x, short y, short z)
    {
      Blocks[x, y, z] = block;
    }

    public Block Get(Coordinates c)
    {
      return Get((short) c.X, (short) c.Y, (short) c.Z);
    }

    public Block Get(short x, short y, short z)
    {
      return Blocks[x, y, z];
    }

    public Block? GetOrDefault(Coordinates c)
    {
      return TryGet(c, out var block) ? block : default;
    }

    public Block? GetOrDefault(short x, short y, short z)
    {
      return TryGet(x, y, z, out var block) ? block : default;
    }

    public bool TryGet(Coordinates c, out Block? block)
    {
      if (Dimensions.IsInBounds(c))
      {
        block = Get(c);
        return true;
      }

      block = null;
      return false;
    }

    public bool TryGet(short x, short y, short z, out Block? block)
    {
      if (Dimensions.IsInBounds(x, y, z))
      {
        block = Get(x, y, z);
        return true;
      }

      block = null;
      return false;
    }
  }
}