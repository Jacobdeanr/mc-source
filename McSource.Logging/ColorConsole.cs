using System;
using System.Collections.Generic;

namespace McSource.Logging
{
  public class ColorConsole
  {
    #region Public Fields

    public static readonly Dictionary<LogLevel, ConsoleColor> Colors = new Dictionary<LogLevel, ConsoleColor>
    {
      { LogLevel.Critical, ConsoleColor.DarkRed },
      { LogLevel.Error, ConsoleColor.Red },
      { LogLevel.Warning, ConsoleColor.Yellow },
      { LogLevel.Information, ConsoleColor.Cyan },
      { LogLevel.Trace, ConsoleColor.DarkBlue },
      { LogLevel.Debug, ConsoleColor.Blue }
    };

    #endregion Public Fields

    #region Public Methods

    public static void NewLine()
    {
      Console.WriteLine();
    }

    public static void Write(ColoredString coloredString)
    {
      lock (Console.Out)
      {
        WriteUnblocked(coloredString);
      }
    }

    public static void Write(params ColoredString[] coloredStrings)
    {
      lock (Console.Out)
      {
        foreach (var coloredString in coloredStrings)
        {
          Write(coloredString);
        }
      }
    }

    public static void WriteLine(params ColoredString[] coloredStrings)
    {
      lock (Console.Out)
      {
        foreach (var coloredString in coloredStrings)
        {
          WriteUnblocked(coloredString);
        }

        NewLine();
      }
    }

    public static void WriteUnblocked(ColoredString coloredString)
    {
      // ReSharper disable once ConvertToUsingDeclaration because of net472
      using (new ColorSetter(coloredString))
      {
        Console.Write(coloredString.String);
        if (coloredString.NewLine)
        {
          NewLine();
        }
      }
    }

    #endregion Public Methods
  }
}