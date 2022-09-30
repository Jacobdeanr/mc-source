using VmfSharp;
using VmfSharp.Interfaces;

namespace McSource.Models.Vmf.Abstract
{
  public abstract class VmfIdModel : VmfModel
  {
    public int Id { get; }
    
    protected VmfIdModel(IVmfRoot root) : base(root)
    {
      Id = root.NewId;
    }
  }
}