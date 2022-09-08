using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Models.Vmf.Abstract;
using VmfSharp;

namespace McSource.Models.Vmf
{
  public class Solid : VmfIdModel, IVmfRoot
  {
    public Editor Editor { get; set; }
    public ICollection<Side> Sides { get; set; } = Array.Empty<Side>();

    public Solid([NotNull] IVmfRoot root) : base(root)
    {
    }

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("solid");
      rootTag.Value.Add(new VmfInt("id", Id));
      rootTag.Value.AddRange(Sides.Select(s => s.ToVmf(indentation + 1)));
      rootTag.Value.Add(Editor.ToVmf(indentation + 1));
      return rootTag;
    }

    private int _idCounter;
    public int NewId => _idCounter++;
  }
}