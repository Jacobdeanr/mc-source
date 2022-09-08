namespace McSource.Logging.Extensions
{
  /// <summary>
  /// Extensions for the <see cref="ColoredString"/> type
  /// </summary>
  public static class ColoredStringExtensions
  {
    #region Public Methods

    /// <summary>
    /// Enable newLine printing.
    /// </summary>
    /// <param name="s">The s.</param>
    /// <returns></returns>
    public static ColoredString NewLine(this ColoredString s)
    {
      s.NewLine = true;
      return s;
    }

    #endregion Public Methods
  }
}