using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace McSource.Models.Nbt.Properties
{
  public struct Property
  {
    public string Name { get; set; }
    public string Value { get; set; }

    public Property(string name, string value)
    {
      Name = name;
      Value = value;
    }

    public static Property FromString(string idString)
    {
      var split = idString.Split('=');
      return new Property()
      {
        Name = split[0],
        Value = split[1]
      };
    }

    public static IEnumerable<Property> FromStringList(string idString)
    {
      return idString.Split(',').Select(FromString);
    }
  }
}