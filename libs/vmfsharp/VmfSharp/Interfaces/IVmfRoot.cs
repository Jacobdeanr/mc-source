namespace VmfSharp.Interfaces
{
  public interface IVmfSerializableRoot : IVmfRoot
  {
    public void ToFile(string filePath);
  }
  
  public interface IVmfRoot
  {
    /// <summary>
    /// Creates and returns an unique VmfProperty Id
    /// </summary>
    /// <returns></returns>
    public int NewId { get; }
  }
}