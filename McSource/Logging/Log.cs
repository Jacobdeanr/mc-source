
using System;

namespace McSource.Logging
{  public static class Log
  {
    private static void DoLog(LogLevel logLevel, object? data)
    {
      Console.WriteLine($"{logLevel.ToString()[0]}: {data?.ToString()}");
    }

    public static void Trace(object? data)
    {
      DoLog(LogLevel.Trace, data);
    }

    public static void Debug(object? data)
    {
      DoLog(LogLevel.Debug, data);
    }

    public static void Info(object? data)
    {
      DoLog(LogLevel.Information, data);
    }

    public static void Warning(object? data)
    {
      DoLog(LogLevel.Warning, data);
    }

    public static void Error(object? data)
    {
      DoLog(LogLevel.Error, data);
    }

    public static void Error(object? data, Exception e)
    {
      Error($"{e.Message}: {data}");
      Console.Error.WriteLine(e.StackTrace);
    }

    public static void Critical(object? data, Exception e)
    {
      Critical($"{e.Message}: {data}");
      Console.Error.WriteLine(e.StackTrace);
    }

    public static void Critical(object? data)
    {
      DoLog(LogLevel.Critical, data);
    }
  }

}
