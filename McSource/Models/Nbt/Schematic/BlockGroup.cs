using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Schematic
{
  public class BlockGroup : IVmfModelConvertible<Solid>
  {
    public McDirection3D Direction { get; set; }
    private Block _start;
    private Block? _end;

    public int Length { get; private set; } = 1;

    public void Add(params Block[] blocks)
    {
      foreach (var block in blocks)
      {
        Length++;
      
        _end = block;
        _end.BlockGroup = this;
      }
    }

    public BlockGroup(McDirection3D direction, Block start, params Block[] end) : this(direction, start)
    {
      Add(end);
    }
    
    public BlockGroup(McDirection3D direction, Block start)
    {
      Direction = direction;
      
      _start = start;
      _start.BlockGroup = this;
    }

    public Solid? ToModel(IVmfRoot root)
    {
      _start.Extend(Length - 1, Direction);
      return _start.ToModel(root);
    }

    public override string ToString()
    {
      var str = $"[{Length}] {_start}";
      if (_end != null)
      {
        str += $"to {_end}";
      }
      return str;
    }
  }
}