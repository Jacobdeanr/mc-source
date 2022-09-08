using JetBrains.Annotations;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

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

    public Plane([NotNull] IVmfRoot root) : base(root)
    {
    }
  }
}