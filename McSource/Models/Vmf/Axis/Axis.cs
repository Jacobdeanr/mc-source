using System.Globalization;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models.Vmf.Axis
{
  /// <summary>
  /// <para>Base class for axes</para>
  /// <para>Source: <a href="https://developer.valvesoftware.com/wiki/Valve_Map_Format#U.2FV_Axis">Valve Developer Community</a></para>
  /// </summary>
  public abstract class Axis : VmfModel
  {
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public int Translation { get; set; }

    public double Scaling { get; set; }

    protected Axis(IVmfRoot root, int x, int y, int z, int translation, double scaling) : base(root)
    {
      X = x;
      Y = y;
      Z = z;

      Translation = translation;
      Scaling = scaling;
    }

    public override string ToString()
    {
      return $"[{X} {Y} {Z} {Translation}] {Scaling.ToString(CultureInfo.GetCultureInfo("en-US"))}";
    }
  }
}