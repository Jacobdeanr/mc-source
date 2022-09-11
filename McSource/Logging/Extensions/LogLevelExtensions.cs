namespace McSource.Logging.Extensions
{
  /// <summary>
  /// Extensions for the <see cref="LogLevel"/> type
  /// </summary>
  public static class LogLevelExtensions
  {

    /// <summary>
    /// GetSide the initial character of the LogLevel name
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <returns></returns>
    public static char FirstChar(this LogLevel logLevel)
    {
      return logLevel.ToString()[0];
    }

  }
}