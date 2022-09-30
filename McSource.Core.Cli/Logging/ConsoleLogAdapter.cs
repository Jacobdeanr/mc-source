using System;
using System.Threading;
using McSource.Logging;
using McSource.Logging.Extensions;

namespace McSource.Core.Cli.Logging
{
  
    /// <summary>
    /// <see cref="ILogAdapter"/> for logging to the console
    /// </summary>
    public class ConsoleLogAdapter : ILogAdapter
    {
        public bool PrintThreadInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogAdapter"/> class.
        /// </summary>
        public ConsoleLogAdapter(bool printThreadInfo = false)
        {
            PrintThreadInfo = printThreadInfo;
        }

        public void OnLog(LogLevel logLevel, object? data)
        {
            var content = new[]
            {
                $"{logLevel.FirstChar()}: ".Color(logLevel),
                $"{DateTime.Now.ToShortTimeString()} | ".Color(ConsoleColor.DarkGray),
                PrintThreadInfo
                    ? $"{ThreadName} | ".Color(ConsoleColor.DarkGray)
                    : string.Empty.Neutral(),
                data?.ToString().NewLine() ?? string.Empty.Neutral()
            };

            ColorConsole.Write(content);
        }

        private string ThreadName => Thread.CurrentThread.Name ?? Environment.CurrentManagedThreadId.ToString();
    }
}
