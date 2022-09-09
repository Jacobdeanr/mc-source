﻿using VmfSharp;

namespace McSource.Models.Vmf.Axis
{
  /// <summary>
  /// <para>Represents the X Axis</para>
  /// <para>Source: <a href="https://developer.valvesoftware.com/wiki/Valve_Map_Format#U.2FV_Axis">Valve Developer Community</a></para>
  /// <seealso cref="Axis"/>
  /// </summary>
  public class UAxis : Axis
  {
    public override IVmf ToVmf(int indentation)
    {
      return new VmfString("uaxis", ToString());
    }

    public UAxis(IVmfRoot root, int x, int y, int z, int translation, double scaling)
      : base(root, x, y, z, translation, scaling)
    {
    }
  }
}