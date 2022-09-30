using System;

namespace McSource.Models.Nbt
{
  public class Dimensions2D : IEquatable<Dimensions2D>
  {
    public short DX { get; set; }
    public short DZ { get; set; }

    public Dimensions2D()
    {
    }

    public Dimensions2D(short size)
      : this(size, size)
    {
    }

    public Dimensions2D(short dx, short dz)
    {
      DX = dx;
      DZ = dz;
    }

    public bool Equals(Dimensions2D? other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return DX == other.DX && DZ == other.DZ;
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != this.GetType())
      {
        return false;
      }

      return Equals((Dimensions2D) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(DX, DZ);
    }
  }
}