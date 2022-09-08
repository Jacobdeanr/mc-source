using JetBrains.Annotations;
using VmfSharp;

namespace McSource.Models.Vmf.Abstract
{
  public abstract class VmfIdModel : VmfModel
  {
    public int Id { get; }
    
    protected VmfIdModel([NotNull] IVmfRoot root) : base(root)
    {
      Id = root.NewId;
    }
  }
}