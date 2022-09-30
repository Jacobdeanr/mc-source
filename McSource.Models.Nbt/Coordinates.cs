using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VmfSharp;
using VmfSharp.Models.Structs;

namespace McSource.Models.Nbt
{
  public struct Coordinates : IEquatable<Coordinates>
  {
    /// <summary>
    /// FaceDirection on the horizontal X axis (West - East) (Red)
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// FaceDirection on the vertical Y axis (Bottom - Top) (Green)
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// FaceDirection on the horizontal Z axis (North - South) (Blue)
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

    public Coordinates(IReadOnlyList<int> pos)
    {
      if (pos.Count != 3)
      {
        throw new ArgumentException("Invalid FaceDirection array");
      }

      X = pos[0];
      Y = pos[1];
      Z = pos[2];
    }

    public Coordinates(Coordinates c) : this(c.X, c.Y, c.Z)
    {
    }

    public Coordinates Move(int x, int y, int z)
    {
      return MoveX(x).MoveY(y).MoveZ(z);
    }

    public Coordinates MoveX(int amount)
    {
      X += amount;
      return this;
    }

    public Coordinates MoveY(int amount)
    {
      Y += amount;
      return this;
    }

    public Coordinates MoveZ(int amount)
    {
      Z += amount;
      return this;
    }

    public override string ToString()
    {
      return $"x: {X}, y: {Y}, z:{Z}";
    }

    public static Coordinates operator +(Coordinates c1, Coordinates c2)
    {
      c1.X += c2.X;
      c1.Y += c2.Y;
      c1.Z += c2.Z;
      return c1;
    }

    public static Coordinates operator *(Coordinates c1, Coordinates c2)
    {
      c1.X *= c2.X;
      c1.Y *= c2.Y;
      c1.Z *= c2.Z;
      return c1;
    }

    public static Coordinates operator *(Coordinates c1, int amount)
    {
      c1.X *= amount;
      c1.Y *= amount;
      c1.Z *= amount;
      return c1;
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

    public Coordinates Clone()
    {
      return new Coordinates(this);
    }

    public Vertex ToVertex(double offsetX = 0, double offsetY = 0, double offsetZ = 0)
    {
      return new Vertex(X + offsetX, Z + offsetZ, Y + offsetY);
    }
  }
}