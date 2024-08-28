using VmfSharp.Interfaces;

namespace VmfSharp.Models.Property
{
  public class Vmf<TValue> : IVmf
  {
    public string Name { get; }
    public TValue Value { get; set; }

    public Vmf(string name) => Name = name;
    public Vmf(string name, TValue value) : this(name) => Value = value;

    public virtual string GetValueString() => Value?.ToString() ?? string.Empty;

    public override string ToString() => ToString(0);

    public string ToString(int indentation) => ToString(GetIndentPadding(indentation));
    public virtual string ToString(string indentPadding) => indentPadding + $"\"{Name}\" \"{GetValueString()}\"";
    protected static string GetIndentPadding(int indentation) => new string('\t', indentation);
  }
}