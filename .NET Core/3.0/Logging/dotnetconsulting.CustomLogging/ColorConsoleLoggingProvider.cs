// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace dotnetconsulting.CustomLogging
{
    public class ColorConsoleLoggingProvider : ILoggerProvider
    {
        // Konfiguration
        private readonly ColorConsoleLoggerConfiguration _configuration;

        // Pro Category (Loggende Klasse) ein Logger
        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers = new ConcurrentDictionary<string, ColorConsoleLogger>();


        public ColorConsoleLoggingProvider(ColorConsoleLoggerConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            // Pro Category (Loggende Klasse) ein Logger
            return _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(categoryName, _configuration));
        }

        #region Dispose-Pattern
        // Flag: Has Dispose already been called?
        private bool disposed = false;
        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}