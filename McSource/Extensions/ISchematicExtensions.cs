using McSource.Models.Nbt;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Schematic;

namespace McSource.Extensions
{
  public static class ISchematicExtensions
  {
    public static bool TryGet(this ISchematic self, Coordinates c, out Block? block)
    {
      if (self.Dimensions.IsInBounds(c))
      {
        block = self.Get(c);
        return true;
      }

      block = null;
      return false;
    }

    public static bool TryGet(this ISchematic self, int x, int y, int z, out Block? block)
    {
      return self.TryGet((short) x, (short) y, (short) z, out block);
    }

    public static Block? GetOrDefault(this ISchematic self, Coordinates c)
    {
      return self.TryGet(c, out var block) ? block : default;
    }

    public static Block? GetOrDefault(this ISchematic self, int x, int y, int z)
    {
      return self.TryGet((short) x, (short) y, (short) z, out var block) ? block : default;
    }

    public static Block Get(this ISchematic self, Coordinates c)
    {
      return self.Get(c.X, c.Y, c.Z);
    }

    public static Block Get(this ISchematic self, int x, int y, int z)
    {
      return self.Get((short) x, (short) y, (short) z);
    }

    public static void Add(this ISchematic self, Block block, Coordinates c)
    {
      self.Add(block, c.X, c.Y, c.Z);
    }

    public static void Add(this ISchematic self, Block block, int x, int y, int z)
    {
      self.Add(block, (short) x, (short)y,(short) z);
    }
  }
}