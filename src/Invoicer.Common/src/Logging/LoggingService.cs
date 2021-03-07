using Invoicer.Common.Extensions;

namespace Invoicer.Common.Logging
{
    public interface ILoggingService
    {
        public void SetLoggingLevel(string logEventLevel)
            => LoggingExtensions.LoggingLevelSwitch.MinimumLevel = LoggingExtensions.GetLogEventLevel(logEventLevel);
    }
    public class LoggingService : ILoggingService {}
}