using System;

namespace McSource.Logging
{
  /// <summary>
  /// Colored string model
  /// </summary>
  public struct ColoredString
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the color of the console.
    /// </summary>
    /// <value>The color of the console.</value>
    public ConsoleColor ConsoleColor { get; set; }

    /// <summary>
    /// Print a new line.
    /// </summary>
    /// <value><c>true</c> if [new line]; otherwise, <c>false</c>.</value>
    public bool NewLine { get; set; }

    /// <summary>
    /// Gets or sets the string.
    /// </summary>
    /// <value>The string.</value>
    public string? String { get; set; }

    #endregion Public Properties
  }
}