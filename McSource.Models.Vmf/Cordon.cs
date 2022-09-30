using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;
using VmfSharp.Models.Structs;

namespace McSource.Models.Vmf
{
  public class Cordon : VmfModel
  {
    public Vertex Mins { get; } = new Vertex(-1024, -1024, -1024);
    public Vertex Maxs { get; } = new Vertex(-1024, -1024, -1024);
    public bool Active { get; } = false;

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("cordon");
      rootTag.Value.Add(new VmfVertex("mins", Mins));
      rootTag.Value.Add(new VmfVertex("maxs", Maxs));
      rootTag.Value.Add(new VmfBool("active", Active));
      return rootTag;
    }

    public Cordon(IVmfRoot root) : base(root)
    {
    }
  }
}