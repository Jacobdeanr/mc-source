using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;

namespace McSource.Models.Vmf
{
  public class VisGroups : VmfModel
  {
    public override IVmf ToVmf(int indentation)
    {
      return new VmfClass("visgroups");
    }

    public VisGroups(IVmfRoot root) : base(root)
    {
    }
  }
}