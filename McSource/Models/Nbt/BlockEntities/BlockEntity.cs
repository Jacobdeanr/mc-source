using System;
using fNbt;
using McSource.Logging;

namespace McSource.Models.Nbt.BlockEntities
{
  public class BlockEntity
  {
    public string Id { get; set; }
    public Coordinates Coordinates { get; set; }

    protected BlockEntity(string id, Coordinates coordinates)
    {
      Id = id;
      Coordinates = coordinates;
    }

    public static BlockEntity FromTag(NbtCompound tag)
    {
      try
      {
        var id = tag.Get<NbtString>("Id")!.Value;
        var coordinates = Coordinates.FromPos(tag.Get<NbtIntArray>("Pos")!.Value);

        if (tag.TryGet("Text1", out var signText1) &&
            tag.TryGet("Text2", out var signText2) &&
            tag.TryGet("Text3", out var signText3) &&
            tag.TryGet("Text4", out var signText4))
        {
          return new SignBlockEntity(
            id,
            coordinates,
            signText1.StringValue,
            signText2.StringValue,
            signText3.StringValue,
            signText4.StringValue
          );
        }
        
        // https://github.com/SpongePowered/SpongeSchematic-Specification/blob/master/versions/schematic-3.md#blockEntityObject
        // https://minecraft.fandom.com/wiki/Chunk_format#Block_entity_format
        // todo: create additional blockEntity classes for everything that should be visible in the converted map: sign text, skull skins, ...

        return new BlockEntity(id, coordinates);
      }
      catch (NullReferenceException e)
      {
        Log.Error($"Could not parse {nameof(BlockEntities)} from {nameof(NbtCompound)} tag", e);
        throw;
      }
    }
  }
}