using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using McSource.Models.Nbt.Properties;

namespace McSource.Models.Nbt.Structs
{
  public class BlockInfo : IEquatable<BlockInfo>
  {
    public string Id { get; set; } = string.Empty;
    public string Namespace { get; set; } = "minecraft";
    public ICollection<Property> Properties { get; set; } = Array.Empty<Property>();

    public BlockInfo()
    {
      
    }
    
    public BlockInfo(string ns, string id, ICollection<Property> properties) : this(ns, id)
    {
      Properties = properties;
    }

    public BlockInfo(string ns, string id) : this(id)
    {
      Namespace = ns;
    }

    public BlockInfo(string id, ICollection<Property> properties) : this(id)
    {
      Properties = properties;
    }

    public BlockInfo(string id)
    {
      Id = id;
    }

    public static BlockInfo FromString(string str)
    {
      ICollection<Property> properties = Array.Empty<Property>();
      var propSplit = str.Split('[');
      if (propSplit.Length > 1)
      {
        properties = Property.FromStringList(propSplit[1][..^1]).ToArray();
      }

      var idSplit = propSplit[0].Split(":");
      return idSplit.Length > 1
        ? new BlockInfo(idSplit[0], idSplit[1], properties)
        : new BlockInfo(idSplit[0], properties);
    }

    public string ToPath() => $"{Namespace}/{Id}";
    public override string ToString() => $"{Namespace}:{Id} [{Properties?.Count ?? 0} Props]";

    public bool Equals(BlockInfo? other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return Id == other.Id && Namespace == other.Namespace && Properties.Equals(other.Properties);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != this.GetType())
      {
        return false;
      }

      return Equals((BlockInfo) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Id, Namespace, Properties);
    }
  }
}