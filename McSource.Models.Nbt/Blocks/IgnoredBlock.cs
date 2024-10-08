﻿using System;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using VmfSharp.Interfaces;

namespace McSource.Models.Nbt.Blocks
{
  public class IgnoredBlock : Block
  {
    public override bool CanDraw { get; protected set; } = false;

    public IgnoredBlock(ISchematic parent, BlockInfo info, Coordinates coordinates, BlockEntity? blockEntity = default)
      : base(parent, info, coordinates, null, blockEntity)
    {
      Translucent = true;
    }

    public override void Prepare()
    {
    }

    public Solid ToModel(IVmfRoot root)
    {
      throw new NotImplementedException($"{nameof(IgnoredBlock)}s can not be converted to Models");
    }
  }
}