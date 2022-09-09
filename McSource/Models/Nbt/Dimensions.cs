namespace McSource.Models.Nbt
{
  public class Dimensions3D : Dimensions2D
  {
    public short DY { get; set; }

    public Dimensions3D()
    {
    }

    public Dimensions3D(short size)
      : this(size, size, size)
    {
    }

    public Dimensions3D(short dy, short dx, short dz)
    {
      DY = dy;
      DX = dx;
      DZ = dz;
    }

    public bool IsInBounds(Coordinates c)
    {
      return IsInBounds((short) c.X, (short) c.Y, (short) c.Z);
    }

    public bool IsInBounds(short x, short y, short z)
    {
      return x >= 0 && y >= 0 && z >= 0 && x < DX && y < DY && z < DZ;
    }
  }
}