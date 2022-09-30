using McSource.Models.Vmf.Abstract;
using VmfSharp.Interfaces;

namespace McSource.Models.Vmf
{
  public interface IVmfModelConvertible<TModel> where TModel : VmfModel
  {
    public TModel ToModel(IVmfRoot root);
  }
}