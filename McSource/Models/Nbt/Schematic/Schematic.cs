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

    public IDictionary<NeighborPosition, Block> GetNeighbors(Block block)
    {
      var neighbours = new Dictionary<NeighborPosition, Block>();

      if (TryGet(block.Coordinates.Clone().MoveX(1), out var bWest) && bWest != null)
      {
        neighbours[NeighborPosition.West] = bWest;
      }

      if (TryGet(block.Coordinates.Clone().MoveX(-1), out var bEast) && bEast != null)
      {
        neighbours[NeighborPosition.East] = bEast;
      }

      if (TryGet(block.Coordinates.Clone().MoveY(1), out var bTop) && bTop != null)
      {
        neighbours[NeighborPosition.Top] = bTop;
      }

      if (TryGet(block.Coordinates.Clone().MoveY(-1), out var bBottom) && bBottom != null)
      {
        neighbours[NeighborPosition.Bottom] = bBottom;
      }

      if (TryGet(block.Coordinates.Clone().MoveZ(1), out var bSouth) && bSouth != null)
      {
        neighbours[NeighborPosition.South] = bSouth;
      }

      if (TryGet(block.Coordinates.Clone().MoveZ(-1), out var bNorth) && bNorth != null)
      {
        neighbours[NeighborPosition.North] = bNorth;
      }

      return neighbours;
    }
  }
}