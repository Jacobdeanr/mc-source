using System.Collections.Generic;
using McSource.Models.Nbt.Enums;

namespace McSource.Models.Nbt.Blocks.Components
{
  public class Texture
  {
    private IDictionary<FacePosition, Face> _faces;

    public IReadOnlyCollection<Face> Faces => (IReadOnlyCollection<Face>) _faces.Values;

    public Face GetFace(FacePosition pos)
    {
      return _faces[pos];
    }

    public Texture(Block parent)
    {
      _faces = new Dictionary<FacePosition, Face>()
      {
        {FacePosition.South, new Face(parent, FacePosition.South)},
        {FacePosition.North, new Face(parent, FacePosition.North)},
        {FacePosition.West, new Face(parent, FacePosition.West)},
        {FacePosition.East, new Face(parent, FacePosition.East)},
        {FacePosition.Top, new Face(parent, FacePosition.Top)},
        {FacePosition.Bottom, new Face(parent, FacePosition.Bottom)},
      };
    }

    public Texture(Block parent, string texturePath)
      : this(parent)
    {
      Set(texturePath);
    }

    public Texture(Block parent, string topBotTexturePath, string sideTexturePath)
      : this(parent)
    {
      Set(topBotTexturePath, sideTexturePath);
    }

    public Texture(Block parent, string top, string right, string bottom, string left,
      string front, string rear)
      : this(parent)
    {
      Set(top, right, bottom, left, front, rear);
    }

    public void Set(string texturePath)
    {
      Set(texturePath, texturePath, texturePath, texturePath, texturePath, texturePath);
    }

    public void Set(string topBotTexturePath, string sideTexturePath)
    {
      Set(topBotTexturePath, sideTexturePath, topBotTexturePath, sideTexturePath, sideTexturePath, sideTexturePath);
    }

    public void Set(string top, string right, string bottom, string left, string front, string back)
    {
      GetFace(FacePosition.Top).MaterialPath = top;
      GetFace(FacePosition.South).MaterialPath = back;
      GetFace(FacePosition.West).MaterialPath = left;
      GetFace(FacePosition.North).MaterialPath = front;
      GetFace(FacePosition.East).MaterialPath = right;
      GetFace(FacePosition.Bottom).MaterialPath = bottom;
    }
  }
}