using McSource.Models.Nbt.Blocks.Abstract;

namespace McSource.Models.Nbt.Blocks
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

    public static bool IsUngroupedDrawable(this Block? self)
    {
      return self is {ParentBlockGroup: null, CanDraw: true};
    }

    public static bool IsUngroupedDrawable<T>(this Block? self, out T block) where T : Block
    {
      switch (self)
      {
        case T castBlock:
          block = castBlock;
          return castBlock.IsUngroupedDrawable();
        default:
          block = default!;
          return false;
      }
    }
  }
}