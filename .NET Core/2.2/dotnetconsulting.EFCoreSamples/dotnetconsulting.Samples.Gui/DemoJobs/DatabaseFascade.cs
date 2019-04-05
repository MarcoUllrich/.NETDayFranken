// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class DatabaseFacade : IDemoJob
    {
        private readonly ILogger<DatabaseFacade> _logger;
        private readonly SamplesContext1 _efContext;

        public DatabaseFacade(ILogger<DatabaseFacade> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Query Entities";

        public void Run()
        {
            Debugger.Break();

            // Welcher Provider
            string providerName = _efContext.Database.ProviderName;
            Console.WriteLine(providerName);

            // Stehen Migrations aus?
            IEnumerable<String> openMigrations = _efContext.Database.GetPendingMigrations();

            if (openMigrations.Any())
                // Anwenden
                _efContext.Database.Migrate();

            // Sicherstellen, das DB erzeugt ist
            Debugger.Break();
            _efContext.Database.EnsureCreated();

            // Oder genau das Gegenteil
            Debugger.Break();
            _efContext.Database.EnsureDeleted();

            // Verbindung abgreifen
            DbConnection connetion = _efContext.Database.GetDbConnection();

            // Transaction
            IDbContextTransaction transaction = _efContext.Database.CurrentTransaction;
        }
    }
}