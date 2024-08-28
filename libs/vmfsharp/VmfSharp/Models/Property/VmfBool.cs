namespace VmfSharp.Models.Property
{
  public class VmfBool : Vmf<bool>
  {
    public VmfBool(string name) : base(name)
    {
    }

    public VmfBool(string name, bool value) : base(name, value)
    {
    }

    public override string GetValueString()
    {
      return (Value ? 1 : 0).ToString();
    }
  }
}