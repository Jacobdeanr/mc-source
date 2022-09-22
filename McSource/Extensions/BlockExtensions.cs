
using McSource.Models.Nbt.Blocks.Abstract;

namespace McSource.Extensions
{
  public static class BlockExtensions
  {
    /// <summary>
    /// Returns 'true' if the block is not translucent, false if it is see-through. If the block is null, it is treated as opaque as well.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsOpaque(this Block? self)
    {
      return !(self is {Translucent: true});
    }
  }
}