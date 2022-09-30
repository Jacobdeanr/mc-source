using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;
using VmfSharp.Models.Structs;

namespace McSource.Models.Vmf
{
  public class Editor : VmfModel
  {
    public static Editor Default(IVmfRoot root) =>
      new Vmf.Editor(root)
      {
        Color = new Rgb(0, 180, 0),
        VisGroupShown = true,
        VisGroupAutoShown = true
      };

    public Rgb Color { get; set; } = new Rgb(0, 180, 0);
    public bool VisGroupShown { get; set; } = true;
    public bool VisGroupAutoShown { get; set; } = true;

    public Editor(IVmfRoot root) : base(root)
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