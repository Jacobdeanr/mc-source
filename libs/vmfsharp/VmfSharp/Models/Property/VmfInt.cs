namespace VmfSharp.Models.Property
{
  public class VmfInt : Vmf<int>
  {
    public VmfInt(string name) : base(name)
    {
    }

    public VmfInt(string name, int value) : base(name, value)
    {
    }
  }
}