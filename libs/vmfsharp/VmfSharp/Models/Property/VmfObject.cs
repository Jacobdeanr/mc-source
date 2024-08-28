namespace VmfSharp.Models.Property
{
  public class VmfObject : Vmf<object>
  {
    public VmfObject(string name) : base(name)
    {
    }

    public VmfObject(string name, object value) : base(name, value)
    {
    }
  }
}