using VmfSharp;
using VmfSharp.Interfaces;

namespace McSource.Models.Vmf.Abstract
{
  public abstract class VmfModel : IVmfSerializable
  {
    public IVmfRoot Root { get; }

    public VmfModel(IVmfRoot root)
    {
      Root = root;
    }

    public abstract IVmf ToVmf(int indentation);
  }
}