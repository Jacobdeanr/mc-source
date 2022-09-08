using System;

namespace McSource.Logging
{
  internal class ColorSetter : IDisposable
  {
    #region Public Constructors

    public ColorSetter(ColoredString coloredString)
    {
      SetConsoleColor(coloredString);
    }

    public ColorSetter(ConsoleColor color)
    {
      SetConsoleColor(color);
    }

    #endregion Public Constructors

    #region Public Methods

    public void Dispose()
    {
      Console.ResetColor();
    }

    #endregion Public Methods

    #region Private Methods

    private static void SetConsoleColor(ColoredString coloredString)
    {
      SetConsoleColor(coloredString.ConsoleColor);
    }

    private static void SetConsoleColor(ConsoleColor consoleColor)
    {
      if (consoleColor == default)
      {
        return;
      }

      Console.ForegroundColor = consoleColor;
    }

    #endregion Private Methods
  }
}