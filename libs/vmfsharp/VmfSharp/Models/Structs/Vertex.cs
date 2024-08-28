namespace VmfSharp.Models.Structs
{
  /// <summary>
  /// <para>Represents a point in a X(east-west) Y(north-south) Z(top-bottom) coordinate system</para>
  /// <para><a href="https://developer.valvesoftware.com/wiki/Coordinates">Valve Developer Community</a></para>
  /// </summary>
  public struct Vertex
  {
    /// <summary>
    /// 
    /// </summary>
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vertex(double x, double y, double z)
    {
      X = x;
      Y = y;
      Z = z;
    }
    
    public Vertex(Vertex c) : this(c.X, c.Y, c.Z)
    {
    }
    
    public static Vertex operator *(Vertex v1, Vertex v2)
    {
      v1.X *= v2.X;
      v1.Y *= v2.Y;
      v1.Z *= v2.Z;
      return v1;
    }

    public static Vertex operator *(Vertex v, int amount)
    {
      v.X *= amount;
      v.Y *= amount;
      v.Z *= amount;
      return v;
    }


    public override string ToString()
    {
      return $"{X} {Y} {Z}";
    }
  }
}