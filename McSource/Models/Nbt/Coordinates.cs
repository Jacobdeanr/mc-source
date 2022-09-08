using System;

namespace McSource.Models.Nbt
{
  public struct Coordinates
  {
    /// <summary>
    /// SidePosition on the horizontal X axis (West - East) (Red)
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// SidePosition on the vertical Y axis (Bottom - Top) (Green)
    /// </summary>
    public int Y { get; set; }
    
    /// <summary>
    /// SidePosition on the horizontal Z axis (North - South) (Blue)
    /// </summary>
    public int Z { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">Sets the <see cref="X"/> position</param>
    /// <param name="y">Sets the <see cref="Y"/> position</param>
    /// <param name="z">Sets the <see cref="Z"/> position</param>
    public Coordinates(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public override string ToString()
    {
      return $"x: {X}⭢, y: {Y}⭡, z:{Z}⭧";
    }

    public static bool operator ==(Coordinates c1, Coordinates c2)
    {
      return c1.Equals(c2);
    }

    public static bool operator !=(Coordinates c1, Coordinates c2)
    {
      return !c1.Equals(c2);
    }

    public override bool Equals(object other)
    {
      return other is Coordinates otherCoordinates && Equals(otherCoordinates);
    }


    public bool Equals(Coordinates other)
    {
      return X == other.X &&
             Y == other.Y &&
             Z == other.Z;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y, Z);
    }

    public static Coordinates FromPos(int[]? pos)
    {
      if (!(pos is { Length: 3 }))
      {
        throw new ArgumentException("Invalid SidePosition array");
      }

      // todo correct coordinate mapping
      return new Coordinates
      {
        X = pos[0],
        Y = pos[1],
        Z = pos[2]
      };
    }
  }
}