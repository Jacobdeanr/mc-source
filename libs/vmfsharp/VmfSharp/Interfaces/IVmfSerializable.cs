namespace VmfSharp.Interfaces
{
  public interface IVmfSerializable
  {
    public IVmf ToVmf(int indentation);
  }
}