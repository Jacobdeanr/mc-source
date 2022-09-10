using System;
using JetBrains.Annotations;
using McSource.Extensions;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using McSource.Models.Vmf.Axis;
using VmfSharp;

namespace McSource.Models.Nbt.Face
{
  public class SolidFace : Face
  {
    public static Face NoDraw(Block b, McPosition3D pos) => new SolidFace(b, pos);

    public SolidFace([NotNull] Block parent, McPosition3D facePosition) : base(parent, facePosition)
    {
    }

    public SolidFace([NotNull] Block parent, McPosition3D facePosition, [NotNull] string materialPath)
      : base(parent, facePosition, materialPath)
    {
    }

    public override Vmf.Side ToModel(IVmfRoot root)
    {
      var model = new Vmf.Side(root)
      {
        Material = MaterialPath
      };

      var coords = new Coordinates(Parent.Coordinates) * 40;
      var pX = Parent.Dimensions.DX;
      var pY = Parent.Dimensions.DY;
      var pZ = Parent.Dimensions.DZ;

      const int translation = 0;
      const double scaling = 0.3125;

      switch (FacePosition.ToValve())
      {
        case ValvePosition3D.Bottom:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(pX, 0, 0)
          );
          break;
        case ValvePosition3D.Top:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;

        case ValvePosition3D.South:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, 0, 0),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;
        case ValvePosition3D.North:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(0, pY, pZ),
            coords.ToVertex(0, 0, pZ)
          );
          break;

        case ValvePosition3D.West:
          model.UAxis = new UAxis(root, 0, -1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, pY, pZ)
          );
          break;
        case ValvePosition3D.East:
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