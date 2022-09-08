namespace McSource.Models.Nbt
{
  public class Dimensions3D : Dimensions2D
  {
    public short Height { get; set; }

    public Dimensions3D()
    {
    }

    public Dimensions3D(short size)
      : this(size, size, size)
    {
    }

    public Dimensions3D(short height, short width, short length)
    {
      Height = height;
      Width = width;
      Length = length;
    }
  }

  public class Dimensions2D
  {
    public short Width { get; set; }
    public short Length { get; set; }

    public Dimensions2D()
    {
    }

    public Dimensions2D(short size)
      : this(size, size)
    {
    }

    public Dimensions2D(short width, short length)
    {
      Width = width;
      Length = length;
    }
  }
}