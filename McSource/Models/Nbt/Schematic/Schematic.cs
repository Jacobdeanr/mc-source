using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Vmf;

namespace McSource.Models.Nbt.Schematic
{
  public abstract class Schematic<T> : ISchematic
  {
    public Config.Config Config { get; protected set; }
    public Block[,,] Blocks { get; protected set; } = new Block[0, 0, 0];
    public Coordinates Offset { get; } = new Coordinates(0, 0, 0);
    public Dimensions3D Dimensions { get; protected set; } = new Dimensions3D();

    public Schematic(Config.Config config)
    {
      Config = config;
    }

    public void Add(Block block, short x, short y, short z)
    {
      Blocks[x, y, z] = block;
    }

    public Block Get(short x, short y, short z)
    {
      return Blocks[x, y, z];
    }

    public Block? GetOrDefault(short x, short y, short z)
    {
      return TryGet(x, y, z, out var block) ? block : default;
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

    public abstract ISchematic Load(T src);
    public abstract Map ToModel();
  }
}