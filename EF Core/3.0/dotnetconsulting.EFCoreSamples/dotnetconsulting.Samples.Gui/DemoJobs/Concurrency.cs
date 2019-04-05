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
using System.Data.SqlClient;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class Concurrency : IDemoJob
    {
        private readonly ILogger<Concurrency> _logger;
        private readonly SamplesContext1 _efContext;

        public Concurrency(ILogger<Concurrency> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Concurrency";

        public void Run()
        {
            Debugger.Break();

            int speakerId = 2;

            // Entität mit Id laden und verändern
            Speaker speaker = _efContext.Find<Speaker>(speakerId);

            // Attribut muss Concurrency-Token oder RowVersion sein
            speaker.Updated = DateTime.Now;
            Thread.Sleep(250);

            // Attribut (Spalte) direkt in der Datenbank ändern
            const string SQLUPDATE = "UPDATE [dnc].[Speakers] SET Updated = GETDATE() WHERE Id = @Id";
            int rowCount = _efContext.Database.ExecuteSqlCommand(SQLUPDATE, new SqlParameter("Id", speakerId));

            SolveMode mode = SolveMode.ClientWins;
            bool savingFailed = false;
            do
            {
                try
                {
                    // Nun Entität speichern
                    // Es sollte zu einer DbUpdateConcurrencyException kommen
                    _efContext.SaveChanges();
                    savingFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    savingFailed = true;

                    switch (mode)
                    {
                        case SolveMode.ClientWins:
                            // Client wins
                            ex.Entries.ToList().ForEach(e => e.OriginalValues.SetValues(e.GetDatabaseValues()));
                            break;
                        case SolveMode.StoreWins:
                            ex.Entries.ToList().ForEach(e => e.Reload());
                            break;
                        case SolveMode.Custom:
                            ex.Entries.ToList().ForEach(e =>
                            {
                                var currentValues = e.CurrentValues;
                                var databaseValues = e.GetDatabaseValues();

                                var resolvedValues = databaseValues.Clone();

                                // Hier kommt die Logik zur Auflösung der Ausnahme
                                e.OriginalValues.SetValues(databaseValues);
                                e.CurrentValues.SetValues(resolvedValues);
                            });
                            break;
                        default:
                            break;
                    }
                }
            } while (savingFailed);
        }

        enum SolveMode
        {
            ClientWins,
            StoreWins,
            Custom
        }
    }
}