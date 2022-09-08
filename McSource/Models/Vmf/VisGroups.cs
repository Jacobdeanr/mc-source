using JetBrains.Annotations;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models.Vmf
{
  public class VisGroups : VmfModel
  {
    public override IVmf ToVmf(int indentation)
    {
      return new VmfClass("visgroups");
    }

    public VisGroups([NotNull] IVmfRoot root) : base(root)
    {
    }
  }
}