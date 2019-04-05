// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Logging;
using System;
using static System.Console;

namespace dotnetconsulting.CustomLogging
{
    public class ColorConsoleLogger : ILogger
    {
        private readonly ColorConsoleLoggerConfiguration _configuration;
        private readonly string _categoryName;

        public ColorConsoleLogger(string categoryName, 
                                  ColorConsoleLoggerConfiguration Configuration)
        {
            _categoryName = categoryName;
            _configuration = Configuration;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            if (state != null)
            {
                ForegroundColor = _configuration.ScopeColor;
                WriteLine($"=== {state} ===");
            }
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Alle Loglevel sind aktiv
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // LogLevel aktiv?
            if (!IsEnabled(logLevel))
                return;

            // Farbe einstellen
            switch (logLevel)
            {
                case LogLevel.Trace:
                    ForegroundColor = _configuration.TraceColor;
                    break;
                case LogLevel.Debug:
                    ForegroundColor = _configuration.DebugColor;
                    break;
                case LogLevel.Information:
                    ForegroundColor = _configuration.InformationColor;
                    break;
                case LogLevel.Warning:
                    ForegroundColor = _configuration.WarningColor;
                    break;
                case LogLevel.Error:
                    ForegroundColor = _configuration.ErrorColor;
                    break;
                case LogLevel.Critical:
                    ForegroundColor = _configuration.CriticalColor;
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }

            // Meldung erzeugen & Ausgabe (ohne Buffer)
            WriteLine($"[{_categoryName}]: {formatter(state, exception)}");
        }
    }
}
