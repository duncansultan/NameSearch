using Serilog;
using System;
using System.Linq;

namespace NameSearch.Extensions
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// SQLs the trace event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void SqlTraceEvent(this ILogger logger, Exception exception, string eventId, string sql,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(sql)) sql = Environment.NewLine + "/* Begin - SQL Command */" + Environment.NewLine + sql.Trim('"') + Environment.NewLine + "/* End - SQL Command */";

            logger.Verbose(exception, "<{EventID:l}> " + sql, allProps);
        }

        /// <summary>
        ///     Verboses the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void VerboseEvent(this ILogger logger, Exception exception, string eventId,
            string messageTemplate, params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Verbose(exception, "<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Verboses the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void VerboseEvent(this ILogger logger, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            logger.Verbose("<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Informations the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void InformationEvent(this ILogger logger, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            logger.Information("<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Informations the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void InformationEvent(this ILogger logger, Exception exception, string eventId,
            string messageTemplate, params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            logger.Information(exception, "<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Debugs the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void DebugEvent(this ILogger logger, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            logger.Debug("<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Debugs the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void DebugEvent(this ILogger logger, Exception exception, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Debug(exception, "<{EventID:l}> " + messageTemplate, allProps);
        }

        /// <summary>
        ///     Warningls the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void WarningEvent(this ILogger logger, Exception exception, string eventId,
            string messageTemplate, params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Warning(exception, "<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Warningls the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void WarningEvent(this ILogger logger, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            logger.Warning("<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Errors the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void ErrorEvent(this ILogger logger, Exception exception, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Error(exception, "<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Fatals the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void FatalEvent(this ILogger logger, Exception exception, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Fatal(exception, "<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Errors the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void ErrorEvent(this ILogger logger, string eventId, string messageTemplate,
            params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Error("<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Fatals the event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="propertyValues">The property values.</param>
        public static void FatalEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            var allProps = new object[] { eventId }.Concat(propertyValues).ToArray();

            if (!string.IsNullOrWhiteSpace(messageTemplate)) messageTemplate = " - " + messageTemplate;

            messageTemplate += " - MemoryUsage {MemoryUsage}";

            logger.Fatal("<{EventID:l}>" + messageTemplate, allProps);
        }

        /// <summary>
        ///     Withes the specified property name.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ILogger With(this ILogger logger, string propertyName, object value)
        {
            return logger.ForContext(propertyName, value, destructureObjects: true);
        }
    }
}
