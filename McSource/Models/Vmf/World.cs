using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using McSource;
using McSource.Models.Vmf;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models.Vmf
{
  public class World : VmfIdModel
  {
    public int MapVersion { get; } = Constants.MapVersion;
    public string ClassName { get; } = "worldspawn";
    public string DetailMaterial { get; } = "detail/detailsprites";
    public string DetailVbsp { get; } = "detail.vbsp";
    public int MaxPropScreenWidth { get; } = -1;
    public string SkyName { get; } = "sky_day02_09";

    public ICollection<IVmfSerializable> Solids { get; set; } = Array.Empty<IVmfSerializable>();

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("world");
      rootTag.Value.Add(new VmfInt("id", Id));
      rootTag.Value.Add(new VmfInt("mapversion", MapVersion));
      rootTag.Value.Add(new VmfString("classname", ClassName));
      rootTag.Value.Add(new VmfString("detailmaterial", DetailMaterial));
      rootTag.Value.Add(new VmfString("detailvbsp", DetailVbsp));
      rootTag.Value.Add(new VmfInt("maxpropscreenwidth", MaxPropScreenWidth));
      rootTag.Value.Add(new VmfString("skyname", SkyName));

      foreach (var solid in Solids)
      {
        if (solid == null)
        {
          continue;
        }

        rootTag.Value.Add(solid.ToVmf(indentation));

      }

      return rootTag;
    }

    public World([NotNull] IVmfRoot root) : base(root)
    {
    }
  }
}