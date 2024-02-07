using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using System;
using System.Text;

namespace EAU.Web.EDocViewer.App
{
    public static class SerilogJsonFormatExtention
    {
        public static LoggerConfiguration JsonFile(
            this LoggerSinkConfiguration sinkConfiguration
            , string path
            , bool renderMessage = false
            , LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose
            , long? fileSizeLimitBytes = 1073741824
            , LoggingLevelSwitch levelSwitch = null
            , bool buffered = false
            , bool shared = false
            , TimeSpan? flushToDiskInterval = null
            , RollingInterval rollingInterval = RollingInterval.Infinite
            , bool rollOnFileSizeLimit = false
            , int? retainedFileCountLimit = 31
            , Encoding encoding = null
            , FileLifecycleHooks hooks = null)
        {
            ITextFormatter formatter = renderMessage ? new JsonFormatter(renderMessage: renderMessage) : new JsonFormatter();

            return sinkConfiguration.File(
                formatter
                , path
                , restrictedToMinimumLevel
                , fileSizeLimitBytes
                , levelSwitch
                , buffered
                , shared
                , flushToDiskInterval
                , rollingInterval
                , rollOnFileSizeLimit
                , retainedFileCountLimit
                , encoding
                , hooks);
        }
    }
}
