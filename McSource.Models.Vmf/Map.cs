using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using McSource.Models.Vmf.Abstract;
using VmfSharp;
using VmfSharp.Interfaces;

namespace McSource.Models.Vmf
{
  /// <summary>
  /// <para>DTO for a Map according to the Valve Map Format. </para>
  /// <para>Source: <a href="https://developer.valvesoftware.com/wiki/Valve_Map_Format">Valve Developer Community</a></para>
  /// </summary>
  public class Map : IVmfSerializableRoot
  {
    public VersionInfo VersionInfo { get; set; }
    public VisGroups VisGroups { get; set; }
    public ViewSettings ViewSettings { get; set; }
    public World World { get; set; }
    public Cordon Cordon { get; set; }

    public ICollection<Entity> Entities { get; set; } = Array.Empty<Entity>();

    public Map()
    {
      Cordon = new Cordon(this);
      VisGroups = new VisGroups(this);
      VersionInfo = new VersionInfo(this);
      ViewSettings = new ViewSettings(this);
    }
    
    public Map(World world) : this()
    {
      World = world;
    }
    
    #region IVmfRoot

    private int _idCounter;

    public int NewId => ++_idCounter;
    
    public void ToFile(string filePath)
    {
      var classes = new List<IVmfSerializable>();
      classes.Add(VersionInfo);
      classes.Add(VisGroups);
      classes.Add(ViewSettings);
      classes.Add(World);
      classes.AddRange(Entities);
      classes.Add(Cordon);

      using StreamWriter outfile = new StreamWriter(filePath);
      foreach (var c in classes.Select(v => v.ToVmf(0)))
      {
        outfile.WriteLine(c.ToString()); 
      }
    }

    #endregion
  }
}