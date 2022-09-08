using System;

namespace McSource.Logging.Extensions
{
  /// <summary>
  /// Extensions for the <see cref="string"/> type
  /// </summary>
  public static class StringExtensions
  {
    #region Public Methods

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">       The string.</param>
    /// <param name="logLevel">The log level.</param>
    /// <returns></returns>
    public static ColoredString Color(this string s, LogLevel logLevel)
    {
      return s.ToColoredString(logLevel);
    }

    /// <summary>
    /// Colors the specified console color.
    /// </summary>
    /// <param name="s">           The string.</param>
    /// <param name="consoleColor">Color of the console.</param>
    /// <returns></returns>
    public static ColoredString Color(this string s, ConsoleColor consoleColor)
    {
      return s.ToColoredString(consoleColor);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Critical(this string s)
    {
      return s.ToColoredString(LogLevel.Critical);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Debug(this string s)
    {
      return s.ToColoredString(LogLevel.Debug);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Error(this string s)
    {
      return s.ToColoredString(LogLevel.Error);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Information(this string s)
    {
      return s.ToColoredString(LogLevel.Information);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Neutral(this string s)
    {
      return s.ToColoredString();
    }

    /// <summary>
    /// Creates a <see cref="ColoredString"/> which prints a NewLine at it's end.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString NewLine(this string? s)
    {
      return s.ToColoredString().NewLine();
    }

    /// <summary>
    /// Converts to colored string.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString ToColoredString(this string? s)
    {
      return new ColoredString
      {
        String = s
      };
    }

    /// <summary>
    /// Converts to colored string.
    /// </summary>
    /// <param name="s">    The string.</param>
    /// <param name="color">The color.</param>
    /// <returns></returns>
    public static ColoredString ToColoredString(this string s, ConsoleColor color)
    {
      var coloredString = s.ToColoredString();
      coloredString.ConsoleColor = color;
      return coloredString;
    }

    /// <summary>
    /// Converts to colored string.
    /// </summary>
    /// <param name="s">       The string.</param>
    /// <param name="logLevel">The log level.</param>
    /// <returns></returns>
    public static ColoredString ToColoredString(this string s, LogLevel logLevel)
    {
      var coloredString = s.ToColoredString();
      coloredString.ConsoleColor = ColorConsole.Colors.ValueOrDefault(logLevel);
      return coloredString;
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Trace(this string s)
    {
      return s.ToColoredString(LogLevel.Trace);
    }

    /// <summary>
    /// Set the string color based on the specified <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns></returns>
    public static ColoredString Warning(this string s)
    {
      return s.ToColoredString(LogLevel.Warning);
    }

    #endregion Public Methods
  }
}