﻿using System;
using System.Linq;
using fNbt;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models.Structs;

namespace McSource.Models.Nbt.Blocks
{
  public class SkyboxBlock : SolidBlock
  {
    private static readonly BlockInfo BlockInfo = new BlockInfo("tools", "toolsskybox");
    private static readonly string MaterialPath = BlockInfo.ToPath();

    public SkyboxBlock([NotNull] ISchematic parent, Coordinates coordinates, Dimensions3D dimensions) : base(parent, BlockInfo, coordinates,
      null)
    {
      Translucent = true;
      Dimensions = dimensions;
    }

    public override void Prepare()
    {
    }

    public override Solid ToModel(IVmfRoot root)
    {
      var solid = new Vmf.Solid(root);
      solid.Sides = Enum.GetValues(typeof(McDirection3D)).Cast<McDirection3D>().Select(pos => GetFace(pos).ToModel(solid)).ToArray();
      solid.Editor = new Editor(solid)
      {
        Color = new Rgb(0, 180, 0),
        VisGroupShown = true,
        VisGroupAutoShown = true
      };
      return solid;
    }

    protected override Face.Face GetFace(McDirection3D pos)
    {
      return new SolidFace(this, pos, MaterialPath);
    }
  }
}