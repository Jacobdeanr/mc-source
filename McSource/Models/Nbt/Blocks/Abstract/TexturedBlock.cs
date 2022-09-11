using System;
using System.Collections.Generic;
using System.Linq;
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
  public abstract class TexturedBlock : Block
  {
    protected TexturedBlock([NotNull] ISchematic parent, BlockInfo info, Coordinates coordinates,
      [CanBeNull] BlockEntity? blockEntity = default) : base(parent, info, coordinates, blockEntity)
    {
    }

    public override Vmf.Solid? ToModel(IVmfRoot root)
    {
      var solid = new Vmf.Solid(root);
      
      if (BlockGroup == null)
      {
        // todo merge neighbour information when added to blockgroup

        var neighbors = GetNeighbors();

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
      }
      else
      {
        solid.Sides = Enum.GetValues(typeof(McDirection3D))
          .Cast<McDirection3D>()
          .Select(d => GetFace(d).ToModel(solid))
          .ToArray();
      }

      solid.Editor = new Editor(solid)
      {
        Color = new Rgb(0, 180, 0),
        VisGroupShown = true,
        VisGroupAutoShown = true
      };

      return solid;
    }

    protected abstract Face.Face GetFace(McDirection3D pos);
  }
}