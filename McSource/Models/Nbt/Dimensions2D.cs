namespace McSource.Models.Nbt
{
  public class Dimensions2D
  {
    public short DX { get; set; }
    public short DZ { get; set; }

    public Dimensions2D()
    {
    }

    public Dimensions2D(short size)
      : this(size, size)
    {
    }

    public Dimensions2D(short dx, short dz)
    {
      DX = dx;
      DZ = dz;
    }
  }
}