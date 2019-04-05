// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu 

using dotnetconsulting.Samples.EFContext;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Diagnostics;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class DbFunction : IDemoJob
    {
        private readonly ILogger<DbFunction> _logger;
        private readonly SamplesContext1 _efContext;

        public DbFunction(ILogger<DbFunction> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "DbFunction";

        public void Run()
        {
            Debugger.Break();

            var q = from te in _efContext.TechEvents
                    where SamplesContext1.StringLike(te.Name, "Ba%")
                    select te;

            foreach (var techSession in q)
                Console.WriteLine(techSession);
        }
    }
}