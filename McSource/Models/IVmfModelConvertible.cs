using McSource.Models.Vmf;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models
{
  public interface IVmfModelConvertible<TModel> where TModel : VmfModel
  {
    public TModel ToModel(IVmfRoot root);
  }
}