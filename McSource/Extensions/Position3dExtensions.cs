using McSource.Models.Nbt.Enums;
using VmfSharp;

namespace McSource.Extensions
{
  public static class Position3dExtensions {
    public static McDirection3D ToMc(this ValveDirection3D pos)
    {
      return (McDirection3D) pos;
    }
    
    public static ValveDirection3D ToValve(this McDirection3D pos)
    {
      return (ValveDirection3D) pos;
    }
  }
}