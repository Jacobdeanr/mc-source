using System;
using System.Linq;
using JetBrains.Annotations;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Schematic;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Abstract
{
  /// <summary>
  /// Minecraft Block data model, serialized as func_detail entity
  /// </summary>
  public class DetailBlock : Block, IVmfModelConvertible<Vmf.Entity>
  {
    public DetailBlock([NotNull] ISchematic parent, [NotNull] BlockInfo info, Coordinates coordinates, [CanBeNull] Config.Block? config,
      [CanBeNull] BlockEntity? blockEntity = default) : base(parent, info, coordinates, config, blockEntity)
    {
      Translucent = true;
    }

    public Vmf.Entity ToModel(IVmfRoot root)
    {
      var entity = new Vmf.Entity(root) {ClassName = "func_detail"};
      entity.Solid = GetSolid(entity);
      entity.Editor = Editor.Default(entity);
      return entity;
    }
  }
}