// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.Domains;
using dotnetconsulting.Samples.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class ShadowProperties : IDemoJob
    {
        private readonly ILogger<DatabaseFacade> _logger;
        private readonly SamplesContext1 _efContext;

        public ShadowProperties(ILogger<DatabaseFacade> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Shadow Properties";

        public void Run()
        {
            Debugger.Break();

            int techeventId = 11;
            
            #region Abfragen mit Shadow Property
            Console.Clear();

            TechEvent techEvent1 = _efContext.TechEvents.Find(techeventId);

            // Wert lesen
            string code = (string)_efContext.Entry(techEvent1).Property("Code").CurrentValue;

            // Wert setzen
            _efContext.Entry(techEvent1).Property("Code").CurrentValue = "123456";

            // Änderungen erkannt?
            bool hasChanges = _efContext.ChangeTracker.HasChanges();

            _efContext.SaveChanges();
            #endregion

            #region Abfrage via Shadow Property 
            Console.Clear();
            int techEventsCount1 = _efContext.TechEvents
                                  .Where(w => EF.Property<string>(w, "Code") == "123456")
                                  .Count();

            var techEventsCount2 = _efContext.TechEvents
                                  .OrderBy(o => EF.Property<string>(o, "Code"))
                                  .Count();
            #endregion
        }
    }
}