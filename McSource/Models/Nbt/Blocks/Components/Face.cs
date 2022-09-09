using System;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using McSource.Models.Vmf.Axis;
using VmfSharp;

namespace McSource.Models.Nbt.Blocks.Components
{
  public class Face : IVmfModelConvertible<Vmf.Side>
  {
    public Block Parent { get; }

    public FacePosition FacePosition { get; set; }

    public string MaterialPath { get; set; } = "tools/nodraw";

    public Face(Block parent, FacePosition facePosition)
    {
      Parent = parent;
      FacePosition = facePosition;
    }

    public Face(Block parent, FacePosition facePosition, string materialPath)
      : this(parent, facePosition)
    {
      MaterialPath = materialPath;
    }

    public Vmf.Side ToModel(IVmfRoot root)
    {
      var model = new Vmf.Side(root)
      {
        Material = MaterialPath
      };

      var coords = 
        new Coordinates(Parent.Coordinates.Z, Parent.Coordinates.Y, Parent.Coordinates.X) * 40;
      var pX = Parent.Dimensions.DX;
      var pY = Parent.Dimensions.DY;
      var pZ = Parent.Dimensions.DZ;

      const int translation = 0;
      const double scaling = 0.3125;

      switch (FacePosition)
      {
        case FacePosition.Bottom:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);
          
          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(pX, 0, 0)
          );
          break;
        case FacePosition.Top:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;

        case FacePosition.South:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, 0, 0),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;
        case FacePosition.North:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(0, pY, pZ),
            coords.ToVertex(0, 0, pZ)
          );
          break;

        case FacePosition.West:
          model.UAxis = new UAxis(root, 0, -1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, pY, pZ)
          );
          break;
        case FacePosition.East:
          model.UAxis = new UAxis(root, 0, 1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, 0, pZ)
          );
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }

      return model;
    }
    public Vmf.Side XsdToModel(IVmfRoot root)
    {
      var model = new Vmf.Side(root)
      {
        Material = MaterialPath
      };

      var coords = Parent.Coordinates.Clone() * 40;
      var pX = Parent.Dimensions.DX;
      var pY = Parent.Dimensions.DY;
      var pZ = Parent.Dimensions.DZ;

      const int translation = 0;
      const double scaling = 0.3125;

      switch (FacePosition)
      {
        case FacePosition.Bottom:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(pX, 0, 0)
          );
          break;
        case FacePosition.Top:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;

        case FacePosition.South:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, 0, 0),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;
        case FacePosition.North:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(0, pY, pZ),
            coords.ToVertex(0, 0, pZ)
          );
          break;

        case FacePosition.West:
          model.UAxis = new UAxis(root, 0, -1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, pY, pZ)
          );
          break;
        case FacePosition.East:
          model.UAxis = new UAxis(root, 0, 1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, 0, pZ)
          );
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }

      return model;
    }
    
  }
}