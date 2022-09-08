using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks
{
  public class DefaultBlock : Block
  {
    public DefaultBlock([NotNull] string fullId, Coordinates coordinates,
      [CanBeNull] BlockEntity? blockEntity = default) : base(fullId, coordinates, blockEntity)
    {
    }

    public override Vmf.Solid ToModel(IVmfRoot root)
    {
      var solid = new Vmf.Solid(root);
      solid.Sides = Texture.Faces.Select(f => f.ToModel(solid)).ToArray();
      solid.Editor = new Editor(solid)
      {
        Color = new Rgb(0, 180, 0),
        VisGroupShown = true,
        VisGroupAutoShown = true
      };
      return solid;
    }
  }
}