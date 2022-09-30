using McSource.Models.Vmf.Abstract;
using McSource.Models.Vmf.Axis;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;

namespace McSource.Models.Vmf
{
  public class Side : VmfIdModel
  {
    public Plane Plane { get;set; }
    public string Material { get;set; } = "tools/nodraw";
    public UAxis UAxis { get; set; }
    public VAxis VAxis { get;set; }
    public int Rotation { get;set; } = 0;
    public int LightMapScale { get;set; } = 16;
    public int SmoothingGroups { get;set; } = 0;

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("side");
      rootTag.Value.Add(new VmfInt("id", Id));
      rootTag.Value.Add(Plane.ToVmf(indentation+1));
      rootTag.Value.Add(new VmfString("material", Material));
      rootTag.Value.Add(UAxis.ToVmf(indentation+1));
      rootTag.Value.Add(VAxis.ToVmf(indentation+1));
      rootTag.Value.Add(new VmfInt("rotation", Rotation));
      rootTag.Value.Add(new VmfInt("lightmapscale", LightMapScale));
      rootTag.Value.Add(new VmfInt("smoothing_groups", SmoothingGroups));
      return rootTag;
    }

    public Side(IVmfRoot root) : base(root)
    {
    }
  }
}