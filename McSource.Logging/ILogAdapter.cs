namespace McSource.Logging
{
  public interface ILogAdapter
  {
    public void OnLog(LogLevel logLevel, object? data);
  }
}