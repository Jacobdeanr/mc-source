using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;

namespace McSource.Models.Vmf
{
  public class ViewSettings : VmfModel
  {
    public int BSnapToGrid { get; } = 0;
    public int BShowGrid { get; } = 1;
    public int BShowLogicalGrid { get; } = 0;
    public int NGridSpacing { get; } = McSource.Common.Constants.BlockSize;
    public int BShow3DGrid { get; } = 0;

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("viewsettings");
      rootTag.Value.Add(new VmfInt("bSnapToGrid", BSnapToGrid));
      rootTag.Value.Add(new VmfInt("bShowGrid", BShowGrid));
      rootTag.Value.Add(new VmfInt("bShowLogicalGrid", BShowLogicalGrid));
      rootTag.Value.Add(new VmfInt("nGridSpacing", NGridSpacing));
      rootTag.Value.Add(new VmfInt("bShow3DGrid", BShow3DGrid));
      return rootTag;
    }

    public ViewSettings(IVmfRoot root) : base(root)
    {
    }
  }
}