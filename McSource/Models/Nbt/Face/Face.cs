using McSource.Models.Nbt.Blocks;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Face
{
  public abstract class Face : IVmfModelConvertible<Vmf.Side>
  {
    public Block Parent { get; }

    public McDirection3D FaceDirection { get; set; }

    public string MaterialPath { get; set; } = "tools/toolsnodraw";

    protected Face(Block parent, McDirection3D faceDirection)
    {
      Parent = parent;
      FaceDirection = faceDirection;
    }

    protected Face(Block parent, McDirection3D faceDirection, string materialPath)
      : this(parent, faceDirection)
    {
      MaterialPath = materialPath;
    }

    public abstract Side? ToModel(IVmfRoot root);
  }
}