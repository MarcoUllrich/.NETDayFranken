// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Logging;

namespace dotnetconsulting.ConsoleNLog
{
    public class Application
    {
        private readonly ILogger<Application> _logger;

        public Application(ILogger<Application> Logger)
        {
            _logger = Logger;
        }

        public void Run()
        {
            demoLogging(_logger);
        }

        private void demoLogging(ILogger logger, string scopeName = null)
        {
            // Ohne Scope loggen
            log();

            // Auch mit Scope loggen?
            if (!string.IsNullOrWhiteSpace(scopeName))
            {
                using (logger.BeginScope(scopeName))
                {
                    log();
                }
            }

            void log()
            {
                logger.LogTrace("Trace-Message!");
                logger.LogDebug("Debug-Message!");
                logger.LogInformation("Information-Message!");
                logger.LogWarning("Warning-Message!");
                logger.LogError("Error-Message!");
                logger.LogCritical("Crit Error-Message!");
            }
        }
    }
}