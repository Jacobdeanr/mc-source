using VmfSharp.Models.Structs;

namespace VmfSharp.Models.Property
{
  public class VmfRgb : Vmf<Rgb>
  {
    public VmfRgb(string name) : base(name)
    {
    }

    public VmfRgb(string name, Rgb value) : base(name, value)
    {
    }
  }
}