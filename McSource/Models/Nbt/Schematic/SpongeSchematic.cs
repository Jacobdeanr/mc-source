using System;
using System.Collections.Generic;
using System.Linq;
using fNbt;
using McSource.Logging;
using McSource.Models.Nbt.BlockEntities;
using McSource.Models.Nbt.Blocks;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Structs;
using McSource.Models.Vmf;

namespace McSource.Models.Nbt.Schematic
{
  /// <summary>
  /// Deserializes Schematics from .schem NBT files, according to the <a href="https://github.com/SpongePowered/Schematic-Specification/blob/master/versions/schematic-2.md#paletteObject">Sponge SpongeSchematic Specification</a>
  /// </summary>
  public class SpongeSchematic : Schematic<NbtCompound>
  {
    public static Dimensions3D LoadDimensions(NbtCompound rootTag)
    {
      return new Dimensions3D
      {
        DY = rootTag.Get<NbtShort>("Height")!.Value,
        DX = rootTag.Get<NbtShort>("Width")!.Value,
        DZ = rootTag.Get<NbtShort>("Length")!.Value
      };
    }

    public Coordinates CalcBlockCoordinates(int index, Dimensions3D dimensions)
    {
      // index = (y * length + z) * width + x
      return new Coordinates
      {
        Y = index / (Dimensions.DX * Dimensions.DZ),
        Z = (index % (Dimensions.DX * Dimensions.DZ)) / Dimensions.DX,
        X = (index % (Dimensions.DX * Dimensions.DZ)) % Dimensions.DX,
      };
    }

    private Dictionary<int, string> LoadPalette(NbtCompound rootTag)
    {
      return rootTag
        .Get<NbtCompound>("Palette")!
        .Tags
        .Cast<NbtInt>()
        .ToDictionary(tag => tag.Value, tag => tag.Name!);
    }

    private ICollection<BlockEntity> LoadBlockEntities(NbtCompound rootTag)
    {
      return rootTag
        .Get<NbtList>("BlockEntities")!
        .Select(tag => BlockEntity.FromTag((NbtCompound) tag))
        .ToArray();
    }

    private byte[] LoadBlockData(NbtCompound rootTag)
    {
      return rootTag.Get<NbtByteArray>("BlockData")!.Value;
    }


    // todo cleanup
    public static ISchematic? FromTag(NbtCompound rootTag, Config.Config config)
    {
      try
      {
        return new SpongeSchematic(config).Load(rootTag);
      }
      catch (NullReferenceException e)
      {
        Log.Error($"Could not read SpongeSchematic from {nameof(NbtCompound)}", e);
        return default;
      }
    }

    public SpongeSchematic(Config.Config config) : base(config)
    {
    }

    public Block[] MakeSkyBox(short thickness = 1)
    {
      return new Block[]
      {
        new SkyboxBlock(this, new Coordinates(0, 0, -thickness), new Dimensions3D(Dimensions.DX, Dimensions.DY, thickness)), // South
        new SkyboxBlock(this, new Coordinates(0, 0, Dimensions.DZ), new Dimensions3D(Dimensions.DX, Dimensions.DY, thickness)), // North

        new SkyboxBlock(this, new Coordinates(-thickness, 0, 0), new Dimensions3D(thickness, Dimensions.DY, Dimensions.DZ)), // West
        new SkyboxBlock(this, new Coordinates(Dimensions.DX, 0, 0), new Dimensions3D(thickness, Dimensions.DY, Dimensions.DZ)), // East

        new SkyboxBlock(this, new Coordinates(0, Dimensions.DY, 0), new Dimensions3D(Dimensions.DX, thickness, Dimensions.DZ)), // Top
        new SkyboxBlock(this, new Coordinates(0, -thickness, 0), new Dimensions3D(Dimensions.DX, thickness, Dimensions.DZ)), // Bottom
      };
    }

    public override Map ToModel()
    {
      var solids = new List<Block?>();
      solids.AddRange(MakeSkyBox());
      solids.AddRange(Blocks.Cast<Block?>());

      var map = new Map();
      map.World = new World(map) {Solids = solids.Select(b => b?.ToModel(map)).ToArray()};
      return map;
    }

    /// <summary>
    /// Ported from <a href="https://github.com/SpongePowered/Sponge/blob/aa2c8c53b4f9f40297e6a4ee281bee4f4ce7707b/src/main/java/org/spongepowered/common/data/persistence/SchematicTranslator.java#L147-L175">here</a>
    /// </summary>
    /// <param name="rootTag"></param>
    /// <exception cref="ArgumentException"></exception>
    public override ISchematic Load(NbtCompound rootTag)
    {
      Dimensions = LoadDimensions(rootTag);
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

        var coordinates = CalcBlockCoordinates(index, Dimensions);
        var blockEntity = blockEntities.FirstOrDefault(be => coordinates == be.Coordinates);

        Add(Block.Create(this, BlockInfo.FromString(palette[value]), coordinates, blockEntity), coordinates);

        index++;
      }

      return this;
    }
  }
}