using JetBrains.Annotations;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models.Vmf
{
  public class Editor : VmfModel
  {
    public Rgb Color { get; set; } = new Rgb(0, 180, 0);
    public bool VisGroupShown { get; set; } = true;
    public bool VisGroupAutoShown { get; set; } = true;

    public Editor([NotNull] IVmfRoot root) : base(root)
    {
    }

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("editor");
      rootTag.Value.Add(new VmfString("color", Color.ToString()));
      rootTag.Value.Add(new VmfBool("visgroupshown", VisGroupShown));
      rootTag.Value.Add(new VmfBool("visgroupshown", VisGroupAutoShown));
      return rootTag;
    }
  }
}