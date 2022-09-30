using System;
using fNbt;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Nbt.Enums;
using McSource.Models.Vmf;
using McSource.Models.Vmf.Axis;
using VmfSharp;
using VmfSharp.Interfaces;
using Constants = McSource.Common.Constants;

namespace McSource.Models.Nbt.Face
{
  public class SolidFace : Face
  {
    public static Face NoDraw(Block b, McDirection3D pos) => new SolidFace(b, pos);

    public SolidFace([NotNull] Block parent, McDirection3D faceDirection) : base(parent, faceDirection)
    {
    }

    public SolidFace([NotNull] Block parent, McDirection3D faceDirection, [NotNull] string materialPath)
      : base(parent, faceDirection, materialPath)
    {
    }

    public override Vmf.Side ToModel(IVmfRoot root)
    {
      var model = new Vmf.Side(root)
      {
        Material = MaterialPath
      };

      var coords = new Coordinates(Parent.Coordinates) * Constants.BlockSize;
      var dims = new Dimensions3D(Parent.Dimensions) * Constants.BlockSize;
      var pX = dims.DX;
      var pY = dims.DY;
      var pZ = dims.DZ;

      const int translation = 0;
      const double scaling = 0.3125 / 2;

      switch (FaceDirection.ToValve())
      {
        case ValveDirection3D.Bottom:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(pX, 0, 0)
          );
          break;
        case ValveDirection3D.Top:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, -1, 0, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(pX, pY, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;

        case ValveDirection3D.South:
          model.UAxis = new UAxis(root, 1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, 0, 0),
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, pY, 0)
          );
          break;
        case ValveDirection3D.North:
          model.UAxis = new UAxis(root, -1, 0, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(pX, pY, pZ),
            coords.ToVertex(0, pY, pZ),
            coords.ToVertex(0, 0, pZ)
          );
          break;

        case ValveDirection3D.West:
          model.UAxis = new UAxis(root, 0, -1, 0, translation, scaling);
          model.VAxis = new VAxis(root, 0, 0, -1, translation, scaling);

          model.Plane = new Plane(
            root,
            coords.ToVertex(0, 0, 0),
            coords.ToVertex(0, 0, pZ),
            coords.ToVertex(0, pY, pZ)
          );
          break;
        case ValveDirection3D.East:
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