using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fNbt;
using McSource.Extensions;
using McSource.Logging;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;
using VmfSharp;

namespace McSource.Models.Nbt.Schematic
{
  /// <summary>
  /// Deserializes Schematics from .schem NBT files, according to the <a href="https://github.com/SpongePowered/Schematic-Specification/blob/master/versions/schematic-2.md#paletteObject">Sponge SpongeSchematic Specification</a>
  /// </summary>
  public class SpongeSchematic : Schematic<NbtCompound>
  {
    public SpongeSchematic(Config.Config config) : base(config)
    {
    }

    public SpongeSchematic(Config.Config config, NbtCompound rootTag) : base(config, rootTag)
    {
    }

    /// <summary>
    /// Creates a skybox around the schematic
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    private ICollection<Solid> MakeSkyBox(IVmfRoot map)
    {
      return new[]
      {
        // Skybox: South
        new SkyboxBlock(this, new Coordinates(0, 0, -1), new Dimensions3D(Dimensions.DX, Dimensions.DY, 1)).ToModel(map),
        // Skybox: North
        new SkyboxBlock(this, new Coordinates(0, 0, Dimensions.DZ), new Dimensions3D(Dimensions.DX, Dimensions.DY, 1)).ToModel(map),

        // Skybox: West
        new SkyboxBlock(this, new Coordinates(-1, 0, 0), new Dimensions3D(1, Dimensions.DY, Dimensions.DZ)).ToModel(map),
        // Skybox: East
        new SkyboxBlock(this, new Coordinates(Dimensions.DX, 0, 0), new Dimensions3D(1, Dimensions.DY, Dimensions.DZ)).ToModel(map),

        // Skybox: Top
        new SkyboxBlock(this, new Coordinates(0, Dimensions.DY, 0), new Dimensions3D(Dimensions.DX, 1, Dimensions.DZ)).ToModel(map), // Top
        // Skybox: Bottom
        new SkyboxBlock(this, new Coordinates(0, -1, 0), new Dimensions3D(Dimensions.DX, 1, Dimensions.DZ)).ToModel(map),
      };
    }

    private bool TryGroup(SolidBlock block, int x, int y, int z, McDirection3D direction, out BlockGroup? blockGroup)
    {
      var blocks = new List<SolidBlock>();
      switch (direction)
      {
        case McDirection3D.East:
        case McDirection3D.West:
          var tx = x;
          while (this.TryGet(++tx, y, z, out var nextBlock) && nextBlock.IsUngroupedDrawable<SolidBlock>(out var solidBlock) &&
                 block.Equals(nextBlock))
          {
            blocks.Add(solidBlock);
          }

          break;
        case McDirection3D.North:
        case McDirection3D.South:
          var tz = z;
          while (this.TryGet(x, y, ++tz, out var nextBlock) && nextBlock.IsUngroupedDrawable<SolidBlock>(out var solidBlock) &&
                 block.Equals(nextBlock))
          {
            blocks.Add(solidBlock);
          }

          break;
        case McDirection3D.Top:
        case McDirection3D.Bottom:
          var ty = y;
          while (this.TryGet(x, ++ty, z, out var nextBlock) && nextBlock.IsUngroupedDrawable<SolidBlock>(out var solidBlock) &&
                 block.Equals(nextBlock))
          {
            blocks.Add(solidBlock);
          }

          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }

      if (!blocks.Any())
      {
        blockGroup = default;
        return false;
      }

      blockGroup = new BlockGroup(direction, block, blocks.ToArray());
      return blockGroup.CanDraw;
    }

    public override Map ToModel()
    {
      var map = new Map();
      var solids = new List<Solid>();
      var entities = new List<Entity>();

      // todo inefficient
      // Try to group blocks into BlockGroups
      for (short z = 0; z < Dimensions.DZ; z++)
      for (short x = 0; x < Dimensions.DX; x++)
      for (short y = 0; y < Dimensions.DY; y++)
      {
        if (!TryGet(x, y, z, out var block) || !block.IsUngroupedDrawable<SolidBlock>(out var solidBlock))
        {
          continue;
        }

        if (TryGroup(solidBlock, x, y, z, McDirection3D.Top, out var yGroup) && yGroup != null)
        {
          solids.Add(yGroup.ToModel(map));
          continue;
        }

        if (TryGroup(solidBlock, x, y, z, McDirection3D.East, out var xGroup) && xGroup != null)
        {
          solids.Add(xGroup.ToModel(map));
          continue;
        }

        if (TryGroup(solidBlock, x, y, z, McDirection3D.North, out var zGroup) && zGroup != null)
        {
          solids.Add(zGroup.ToModel(map));
        }
      }

      Log.Info("Solids (grouped):".PadRight(20) + solids.Count.ToString().PadLeft(5));

      var ungroupedCount = 0;
      foreach (var block in Blocks)
      {
        if (!block.IsUngroupedDrawable())
        {
          continue;
        }

        switch (block)
        {
          case SolidBlock solidBlock:
            // Add remaining ungrouped blocks to solids
            ungroupedCount++;
            solids.Add(solidBlock.ToModel(map));
            continue;
          case DetailBlock entityBlock:
            // Add entities to map
            entities.Add(entityBlock.ToModel(map));
            continue;
          default:
            Log.Error($"Failed to add block-type '{block.GetType().Name}' to map");
            break;
        }
      }
      
      Log.Info("Solids (ungrouped):".PadRight(20) + ungroupedCount.ToString().PadLeft(5));

      // Surround with skybox
      var solidsSkyBox = MakeSkyBox(map);
      solids.AddRange(solidsSkyBox);
      Log.Info("Solids (skybox):".PadRight(20) + solidsSkyBox.Count.ToString().PadLeft(5));
      
      Log.Info("Solids (total):".PadRight(20) + solids.Count.ToString().PadLeft(5));
      Log.Info("Entities (total):".PadRight(20) + map.Entities.Count.ToString().PadLeft(5));

      // Assign to world and return map
      map.Entities = entities;
      map.World = new World(map, solids);
      return map;
    }

    /// <summary>
    /// <para>Loads the schematic data from the provided <see cref="NbtCompound"/> root tag</para>
    /// Ported from the Spongepowered <a href="https://github.com/SpongePowered/Sponge/blob/aa2c8c53b4f9f40297e6a4ee281bee4f4ce7707b/src/main/java/org/spongepowered/common/data/persistence/SchematicTranslator.java#L147-L175">SchematicTranslator</a>
    /// </summary>
    /// <param name="rootTag"></param>
    /// <exception cref="ArgumentException"></exception>
    protected override void LoadFromSource(NbtCompound rootTag)
    {
      Dimensions = new Dimensions3D
      {
        DY = rootTag.Get<NbtShort>("Height")!.Value,
        DX = rootTag.Get<NbtShort>("Width")!.Value,
        DZ = rootTag.Get<NbtShort>("Length")!.Value
      };
      Blocks = new Block[Dimensions.DX, Dimensions.DY, Dimensions.DZ];

      var palette = LoadPalette(rootTag);
      var blockEntities = LoadBlockEntities(rootTag);

      var i = 0;
      var index = 0;

      var blockData = LoadBlockData(rootTag);
      while (i < blockData.Length)
      {
        int value = 0, varintLength = 0;

        while (true)
        {
          value |= (blockData[i] & 127) << (varintLength++ * 7);

          if (varintLength > 5)
          {
            throw new ArgumentException("VarInt too big (probably corrupted data)");
          }

          if ((blockData[i++] & 128) != 128)
          {
            break;
          }
        }

        // index = (y * length + z) * width + x
        var coordinates = new Coordinates
        {
          Y = index / (Dimensions.DX * Dimensions.DZ),
          Z = Dimensions.DZ - 1 - ((index % (Dimensions.DX * Dimensions.DZ)) / Dimensions.DX),
          X = (index % (Dimensions.DX * Dimensions.DZ)) % Dimensions.DX,
        };
        var blockEntity = blockEntities.FirstOrDefault(be => coordinates == be.Coordinates);

        this.Add(Block.Create(this, BlockInfo.FromString(palette[value]), coordinates, blockEntity), coordinates);

        index++;
      }
    }

    #region Static Methods (NbtCompound)

    // todo Move SpongeSchematic to dedicated project and convert these methods to extension methods

    private static Dictionary<int, string> LoadPalette(NbtCompound rootTag)
    {
      return rootTag
        .Get<NbtCompound>("Palette")!
        .Tags
        .Cast<NbtInt>()
        .ToDictionary(tag => tag.Value, tag => tag.Name!);
    }

    private static ICollection<BlockEntity> LoadBlockEntities(NbtCompound rootTag)
    {
      return rootTag
        .Get<NbtList>("BlockEntities")!
        .Select(tag => BlockEntity.FromTag((NbtCompound) tag))
        .ToArray();
    }

    private static byte[] LoadBlockData(NbtCompound rootTag)
    {
      return rootTag.Get<NbtByteArray>("BlockData")!.Value;
    }

    #endregion
  }
}