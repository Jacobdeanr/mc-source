﻿namespace VmfSharp.Models.Structs
{
  public struct Rgb
  {
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public Rgb(byte r, byte g, byte b)
    {
      R = r;
      G = g;
      B = b;
    }

    public override string ToString()
    {
      return $"{R} {G} {B}";
    }
  }
}