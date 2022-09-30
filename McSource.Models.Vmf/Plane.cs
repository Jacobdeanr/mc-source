using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models.Property;
using VmfSharp.Models.Structs;

namespace McSource.Models.Vmf
{
  /// <summary>
  /// <para></para>
  /// <para><a>https://developer.valvesoftware.com/wiki/Valve_Map_Format#Planes</a></para>
  /// </summary>
  public class Plane : VmfModel
  {
    public Vertex TopLeft { get; set; }
    public Vertex TopRight { get; set; }
    public Vertex BottomLeft { get; set; }

    public override IVmf ToVmf(int indentation)
    {
      return new VmfString("plane", $"({BottomLeft}) ({TopLeft}) ({TopRight})");
    }

    public Plane(IVmfRoot root) : base(root)
    {
    }

    public Plane(IVmfRoot root, Vertex bottomLeft, Vertex topLeft, Vertex topRight) : this(root)
    {
      TopLeft = topLeft;
      TopRight = topRight;
      BottomLeft = bottomLeft;
    }
  }
}