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
    
    public Dimensions3D(Dimensions3D d) : this(d.DX, d.DY, d.DZ)
    {
    }

    public static Dimensions3D operator +(Dimensions3D d1, Dimensions3D d2)
    {
      d1.DX += d2.DX;
      d1.DY += d2.DY;
      d1.DZ += d2.DZ;
      return d1;
    }

    public static Dimensions3D operator *(Dimensions3D d1, Dimensions3D d2)
    {
      d1.DX *= d2.DX;
      d1.DY *= d2.DY;
      d1.DZ *= d2.DZ;
      return d1;
    }

    public static Dimensions3D operator *(Dimensions3D d1, short amount)
    {
      d1.DX *= amount;
      d1.DY *= amount;
      d1.DZ *= amount;
      return d1;
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