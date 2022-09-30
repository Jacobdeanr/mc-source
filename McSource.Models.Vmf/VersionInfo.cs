using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;

namespace McSource.Models.Vmf
{
  public class VersionInfo : VmfModel
  {
    public int EditorVersion { get; } = 400;
    public int EditorBuild { get; } = 9351;
    public int MapVersion { get; } = Constants.MapVersion;
    public int FormatVersion { get; } = 100;
    public int Prefab { get; } = 0;

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("versioninfo");
      rootTag.Value.Add(new VmfInt("editorversion", EditorVersion));
      rootTag.Value.Add(new VmfInt("editorbuild", EditorBuild));
      rootTag.Value.Add(new VmfInt("mapversion", MapVersion));
      rootTag.Value.Add(new VmfInt("formatversion", FormatVersion));
      rootTag.Value.Add(new VmfInt("prefab", Prefab));
      return rootTag;
    }

    public VersionInfo(IVmfRoot root) : base(root)
    {
    }
  }
}