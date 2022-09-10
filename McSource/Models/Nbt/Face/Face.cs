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

    public McPosition3D FacePosition { get; set; }

    public string MaterialPath { get; set; } = "tools/toolsnodraw";

    protected Face(Block parent, McPosition3D facePosition)
    {
      Parent = parent;
      FacePosition = facePosition;
    }

    protected Face(Block parent, McPosition3D facePosition, string materialPath)
      : this(parent, facePosition)
    {
      MaterialPath = materialPath;
    }

    public abstract Side? ToModel(IVmfRoot root);
  }
}