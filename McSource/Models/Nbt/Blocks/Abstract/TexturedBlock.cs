using System.Collections.Generic;
using JetBrains.Annotations;
using McSource.Logging;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  public abstract class TexturedBlock<TFace> : Block
    where TFace : Face.Face
  {
    protected TexturedBlock([NotNull] ISchematic parent, BlockInfo info, Coordinates coordinates,
      [CanBeNull] BlockEntity? blockEntity = default) : base(parent, info, coordinates, blockEntity)
    {
    }

    protected abstract TFace GetFace(McDirection3D pos);

    public override Vmf.Solid? ToModel(IVmfRoot root)
    {
      var neighbors = GetNeighbors();
      var solid = new Vmf.Solid(root);

      var sides = new List<Vmf.Side>();
      var opaqueNeighborCount = 0;
      foreach (var (position, block) in neighbors)
      {
        if (block == null || block.Translucent)
        {
          // Draw face
          sides.Add(GetFace(position).ToModel(solid));
          continue;
        }

        // Face not visible => don't draw
        opaqueNeighborCount++;
        sides.Add(SolidFace.NoDraw(this, position).ToModel(solid));
      }

      if (opaqueNeighborCount == 6)
      {
        // Completely encased => ignore block
        return null;
      }

      solid.Sides = sides;
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