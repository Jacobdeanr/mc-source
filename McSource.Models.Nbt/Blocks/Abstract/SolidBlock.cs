using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using McSource.Models.Config;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Face;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;
using VmfSharp.Interfaces;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  /// <summary>
  /// Minecraft Block data model, serialized as solid
  /// </summary>
  public class SolidBlock : Block, IVmfModelConvertible<Vmf.Solid>
  {
    public SolidBlock(ISchematic parent, BlockInfo info, Coordinates coordinates, Config.Block? config, BlockEntity? blockEntity = default)
      : base(parent, info, coordinates, config, blockEntity)
    {
    }

    public virtual Vmf.Solid ToModel(IVmfRoot root) => GetSolid(root);
  }
}