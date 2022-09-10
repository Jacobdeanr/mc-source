using McSource.Models.Nbt.Enums;
using VmfSharp;

namespace McSource.Extensions
{
  public static class Position3dExtensions {
    public static McPosition3D ToMc(this ValvePosition3D pos)
    {
      return (McPosition3D) pos;
    }
    
    public static ValvePosition3D ToValve(this McPosition3D pos)
    {
      return (ValvePosition3D) pos;
    }
  }
}