// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace dotnetconsulting.ConsoleLogging
{
    class Program
    {
        // MSDN
        // https://msdn.microsoft.com/de-de/magazine/mt694089.aspx

        // Custom
        // https://asp.net-hacker.rocks/2017/05/05/add-custom-logging-in-aspnetcore.html

        // Nlog: http://edi.wang/post/2017/11/1/use-nlog-aspnet-20
        // Serilog: https://serilog.net/

        static void Main(string[] args)
        {
            string scopeName = "S.C.O.P.E";

            // Demo 1 Einfaches Logging
            LoggingDemo1(scopeName: scopeName);

            // Demo 2 Filter mit Lambda
            // LoggingDemo2(scopeName: scopeName);

            // Demo 3 Serilog File
            // LoggingDemo3(scopeName: scopeName);

            // Demo 4 IConsoleLoggerSettings
            // LoggingDemo4(scopeName: scopeName);

            // Demo 5 Multi Scopes
            // LoggingDemo5(true);

            // Eventlog
            // LoggingDemo6(scopeName: scopeName);

            Console.ReadKey();
        }

        // Einfaches Logging
        static void LoggingDemo1(string scopeName = null)
        {
            Debugger.Break();
            
            // Logger Factory konfigurieren
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Trace, true)
                .AddDebug();

            #region Zukunftsmusik
            //IServiceCollection serviceCollection = new ServiceCollection();

            //// Aktuell nicht möglich ;-(
            //ILoggingBuilder loggingBuilder = new LoggingBuilder(serviceCollection);

            //loggingBuilder.AddConsole(p =>
            //{
            //    p.IncludeScopes = true;
            //})
            //.AddDebug();
            #endregion

            ILogger logger = loggerFactory.CreateLogger<Program>();

            demoLogging(logger, scopeName);
        }

        // Filter mit Lambda-Ausdruck
        static void LoggingDemo2(string scopeName = null)
        {
            Debugger.Break();

            // Logger Factory konfigurieren
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole((text, level) => level >= LogLevel.Warning || text.StartsWith("Idl:"), true)
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            demoLogging(logger, scopeName);
        }

        // Serilog File
        static void LoggingDemo3(string scopeName = null)
        {
            Debugger.Break();

            // Filename für's Logging
            string fileName = Path.Combine(AppContext.BaseDirectory, "Logging.txt");

            // Logger Factory konfigurieren;
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddFile(fileName, LogLevel.Information);

            ILogger logger = loggerFactory.CreateLogger<Program>();

            demoLogging(logger, scopeName);
        }

        // IConsoleLoggerSettings
        static void LoggingDemo4(string scopeName = null)
        {
            Debugger.Break();

            IConsoleLoggerSettings settings = new ConsoleLoggerSettings()
            {
                IncludeScopes = true
            };

            // Logger Factory konfigurieren
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole(settings)
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            demoLogging(logger, scopeName);
        }

        // Multi Scopes
        static void LoggingDemo5(bool includeScopes = true)
        {
            Debugger.Break();

            // Logger Factory konfigurieren
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole(LogLevel.Trace, true)
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            logger.LogDebug("Action!");
            for (int y = 0; y < 2; y++)
            {
                using (logger.BeginScope($"Scope y = {y}"))
                {
                    for (int z = 0; z < 2; z++)
                    {
                        logger.LogDebug($"y = {y}, z = {z}");
                    }
                }
            }
        }

        // Eventlog
        static void LoggingDemo6(string scopeName = null)
        {
            Debugger.Break();

            // Logger Factory konfigurieren
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            // Eventlog gibt es nur unter Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                loggerFactory.AddEventLog(LogLevel.Critical);
            }

            ILogger logger = loggerFactory.CreateLogger<Program>();

            demoLogging(logger, scopeName);
        }

        static void demoLogging(ILogger logger, string scopeName = null)
        {
            Debugger.Break();

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
                logger.Log(LogLevel.Trace, "Trace");
                logger.Log(LogLevel.Debug, "Debug");
                logger.Log(LogLevel.Information, "Information");
                logger.LogWarning("Warning");
                logger.LogError("Error");
                logger.LogCritical("Critical");

                // Wenn etwas schief geht
                try
                {
                    throw new Exception("Passierschein A38 nicht gefunden.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error");
                }

                // Wenn mal etwas richtig schief geht
                try
                {
                    throw new Exception("Passierschein A38 nicht vorhanden.");
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ProgramEventId.PermitNotFound, ex, "Permit A38 not found.");
                }

                // Oder wenn etwas nicht dort ist, wo es sein sollte
                string importFilename = @"c:\nix\app.config";
                try
                {
                    string import = File.ReadAllText(importFilename);
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ProgramEventId.ImportFileFailed, ex, "Import file '{0}' not ", importFilename);
                }
            }
        }
    }
}