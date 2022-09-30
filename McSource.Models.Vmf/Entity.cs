using System;
using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;
using VmfSharp.Models;
using VmfSharp.Models.Property;

namespace McSource.Models.Vmf
{
  public class Entity : VmfIdModel, IVmfRoot
  {
    public Solid Solid { get; set; }
    public Editor Editor { get; set; }
    public string ClassName { get; set; }
    
    public Entity(IVmfRoot root) : base(root)
    {
    }

    public override IVmf ToVmf(int indentation)
    {
      var rootTag = new VmfClass("entity");
      rootTag.Value.Add(new VmfInt("id", Id));
      rootTag.Value.Add(new VmfString("classname", ClassName));
      rootTag.Value.Add(new VmfInt("spawnflags", 0));
      rootTag.Value.Add(Solid.ToVmf(indentation + 1));
      rootTag.Value.Add(Editor.ToVmf(indentation + 1));
      return rootTag;
    }
    
    private int _idCounter;
    public int NewId => ++_idCounter;
  }
}