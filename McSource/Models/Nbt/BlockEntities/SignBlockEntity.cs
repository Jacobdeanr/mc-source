using System;

namespace McSource.Models.Nbt.BlockEntities
{
  public class SignBlockEntity : BlockEntity
  {
    public string[] Lines { get; }

    public SignBlockEntity(string id, Coordinates coordinates, params string[] lines) : base(id, coordinates)
    {
      if (lines.Length > 4)
      {
        throw new ArgumentException("Too many lines of text for this sign!");
      }

      Lines = lines;
    }
  }
}