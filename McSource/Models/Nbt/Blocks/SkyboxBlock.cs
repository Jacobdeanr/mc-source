using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks
{
  public class SkyboxBlock : TexturedBlock<SolidFace>
  {
    private static readonly BlockInfo BlockInfo = new BlockInfo();

    public SkyboxBlock([NotNull] ISchematic parent, Coordinates coordinates, Dimensions3D dimensions) : base(parent, BlockInfo, coordinates)
    {
      Translucent = true;
      Dimensions = dimensions;
    }

    public override Solid? ToModel(IVmfRoot root)
    {
      var solid = new Vmf.Solid(root);
      solid.Sides = Enum.GetValues(typeof(McPosition3D)).Cast<McPosition3D>().Select(pos => GetFace(pos).ToModel(solid)).ToArray();
      solid.Editor = new Editor(solid)
      {
        Color = new Rgb(0, 180, 0),
        VisGroupShown = true,
        VisGroupAutoShown = true
      };
      return solid;
    }

    protected override SolidFace GetFace(McPosition3D pos)
    {
      return new SolidFace(this, pos, "tools/toolsskybox");
    }
  }
}