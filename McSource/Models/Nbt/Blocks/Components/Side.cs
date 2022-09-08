using System;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using McSource.Models.Vmf.Axis;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Components
{
  public class Side : IVmfModelConvertible<Vmf.Side>
  {
    public Block Parent { get; }

    public SidePosition SidePosition { get; set; }

    public Dimensions2D Dimensions { get; private set; }

    public string MaterialPath { get; set; } = "tools/nodraw";

    public Side(Block parent, SidePosition sidePosition)
    {
      SidePosition = sidePosition;
      Parent = parent;
    }

    public Side(Block parent, SidePosition sidePosition, string materialPath)
      : this(parent, sidePosition)
    {
      MaterialPath = materialPath;
      Dimensions = ParseDimensions(SidePosition, parent.Dimensions);
    }

    public Vmf.Side ToModel(IVmfRoot root)
    {
      var model = new Vmf.Side(root)
      {
        Material = MaterialPath
      };

      // todo calc uaxis, vaxis and plane coords
      switch (SidePosition)
      {
        case SidePosition.Top:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        case SidePosition.Bottom:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        case SidePosition.Front:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        case SidePosition.Back:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        case SidePosition.Left:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        case SidePosition.Right:
          model.UAxis = new UAxis(root, 0, 0, 0, 0, 0);
          model.VAxis = new VAxis(root, 0, 0, 0, 0, 0);
          model.Plane = new Plane(root);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return model;
    }

    private static Dimensions2D ParseDimensions(SidePosition pos, Dimensions3D d3d)
    {
      switch (pos)
      {
        case SidePosition.Top:
        case SidePosition.Bottom:
          return new Dimensions2D(d3d.Width, d3d.Length);
        case SidePosition.Front:
        case SidePosition.Back:
          return new Dimensions2D(d3d.Height, d3d.Length); // todo check Length / Width difference
        case SidePosition.Left:
        case SidePosition.Right:
          return new Dimensions2D(d3d.Height, d3d.Width); // todo check Length / Width difference
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}