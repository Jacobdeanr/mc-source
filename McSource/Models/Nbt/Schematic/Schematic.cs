using System;
using fNbt;
using McSource.Logging;
using McSource.Models.Nbt.Blocks.Abstract;
using McSource.Models.Vmf;

namespace McSource.Models.Nbt.Schematic
{
  public abstract class Schematic<T> : ISchematic
  {
    public Config.Config Config { get; }
    public Block[,,] Blocks { get; protected set; }
    public Coordinates Offset { get; protected set; }
    public Dimensions3D Dimensions { get; protected set; }
    
    

    protected Schematic(Config.Config config)
    {
      Config = config;
      Blocks = new Block[0, 0, 0];
      Dimensions = new Dimensions3D();
      Offset = new Coordinates(0, 0, 0);
    }

    protected Schematic(Config.Config config, T src) : this(config)
    {
      Load(src);
    }


    #region Data Loading

    /// <summary>
    /// Loads the schematic data from the provided source object.
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public void Load(T src)
    {
      try
      {
        LoadFromSource(src);
        OnLoaded();
      }
      catch (Exception e)
      {
        Log.Error($"Could not read Schematic '{this.GetType().Name}' from source {nameof(T)}", e);
        throw;
      }
    }

    /// <summary>
    /// Called after the schematic was loaded
    /// </summary>
    public virtual void OnLoaded()
    {
      foreach (var block in Blocks)
      {
        block.Prepare();
      }
    }

    /// <summary>
    /// Load the schematic data from a source object
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    protected abstract void LoadFromSource(T src);

    #endregion

    #region Block Array Operations

    public void Add(Block block, short x, short y, short z)
    {
      Blocks[x, y, z] = block;
    }

    public Block Get(short x, short y, short z)
    {
      return Blocks[x, y, z];
    }

    public Block? GetOrDefault(short x, short y, short z)
    {
      return TryGet(x, y, z, out var block) ? block : default;
    }

    public bool TryGet(short x, short y, short z, out Block? block)
    {
      if (Dimensions.IsInBounds(x, y, z))
      {
        block = Get(x, y, z);
        return true;
      }

      block = null;
      return false;
    }

    #endregion

    public abstract Map ToModel();
  }
}