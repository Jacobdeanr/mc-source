﻿using System;
using System.Collections.Generic;
using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;

namespace McSource.Models.Vmf
{
  public class World : VmfIdModel
  {
    public int MapVersion { get; } = Constants.MapVersion;
    public string ClassName { get; } = "worldspawn";
    public string DetailMaterial { get; } = "detail/detailsprites";
    public string DetailVbsp { get; } = "detail.vbsp";
    public int MaxPropScreenWidth { get; } = -1;
    public string SkyName { get; } = "MCLITE";

    public ICollection<Solid> Solids { get; set; } = Array.Empty<Solid>();

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

    public World(IVmfRoot root) : base(root)
    {
    }
    public World(IVmfRoot root, ICollection<Solid> solids) : base(root)
    {
      Solids = solids;
    }
  }
}