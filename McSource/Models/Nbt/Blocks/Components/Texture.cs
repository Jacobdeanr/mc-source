using System.Collections.Generic;
using McSource.Models.Nbt.Enums;

namespace McSource.Models.Nbt.Blocks.Components
{
  public class Texture
  {
    private IDictionary<SidePosition, Side> _faces;

    public IReadOnlyCollection<Side> Faces => (IReadOnlyCollection<Side>) _faces.Values;

    public Side GetFace(SidePosition pos)
    {
      return _faces[pos];
    }

    public Texture(Block parent)
    {
      _faces = new Dictionary<SidePosition, Side>()
      {
        {SidePosition.Top, new Side(parent, SidePosition.Top)},
        {SidePosition.Bottom, new Side(parent, SidePosition.Bottom)},
        {SidePosition.Left, new Side(parent, SidePosition.Left)},
        {SidePosition.Right, new Side(parent, SidePosition.Right)},
        {SidePosition.Front, new Side(parent, SidePosition.Front)},
        {SidePosition.Back, new Side(parent, SidePosition.Back)},
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
      GetFace(SidePosition.Top).MaterialPath = top;
      GetFace(SidePosition.Back).MaterialPath = back;
      GetFace(SidePosition.Left).MaterialPath = left;
      GetFace(SidePosition.Front).MaterialPath = front;
      GetFace(SidePosition.Right).MaterialPath = right;
      GetFace(SidePosition.Bottom).MaterialPath = bottom;
    }
  }
}