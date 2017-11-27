using System;
using System.Diagnostics;

namespace ArchivesSpace_.Net_Client
{
    public static class AsLogger
    {
        public static void LogInfo(string message)
        {
            LogIt(TraceEventType.Information, message);
        }

        public static void LogDebug(string message)
        {
            LogIt(TraceEventType.Verbose, message);
        }

        public static void LogWarning(string message)
        {
            LogIt(TraceEventType.Warning, message);
        }

        public static void LogWarning(string message, Exception ex)
        {
            var fullMessage = String.Format("Warning: {0}{1}Exception Message: {2}", message, Environment.NewLine, ex);
            LogIt(TraceEventType.Warning, fullMessage);
        }

        public static void LogError(string message)
        {
            LogIt(TraceEventType.Error, message);
        }

        public static void LogError(string message, Exception ex)
        {
            var fullMessage = String.Format("Error: {0}{1}Exception Message: {2}", message, Environment.NewLine, ex);
            LogIt(TraceEventType.Error, fullMessage);
        }

        private static void LogIt(TraceEventType type, string message)
        {
            TraceSource tracer = new TraceSource("aSpace_client");
            if ((Trace.CorrelationManager.ActivityId == Guid.Empty) && (type != TraceEventType.Verbose) && (type != TraceEventType.Information))
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            }

            tracer.TraceEvent(type, 0, message);
        }
    }
}
