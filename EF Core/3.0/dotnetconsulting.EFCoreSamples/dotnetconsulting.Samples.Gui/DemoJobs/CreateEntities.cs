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
    public class CreateEntities : IDemoJob
    {
        private readonly ILogger<QueryEntities> _logger;
        private readonly SamplesContext1 _efContext;

        public CreateEntities(ILogger<QueryEntities> logger,             
                              SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Create Entities";

        public void Run()
        {
            Debugger.Break();

            #region Einzelne Entität
            // Instanz erzeugen 
            Speaker speaker = new Speaker();

            speaker.Name = "Thorsten Kansy";
            speaker.Homepage = "http://www.dotnetcore.eu";

            // An den Context anfügen
            _efContext.Speakers.Add(speaker);
            // oder
            // _efContext.Entry(speaker).State = EntityState.Added;
            // oder 
            // _efContext.Add(speaker);
            // oder
            // _efContext.Attach(speaker);

            try
            {
                // Änderungen speichern (Keine Validierung!)
                _logger.LogInformation($"speaker.Id={speaker.Id}");
                _efContext.SaveChanges();
                _logger.LogInformation($"speaker.Id={speaker.Id}");
            }
            catch (DbUpdateConcurrencyException ex)
            {                
                // Concurrency
                Console.WriteLine(ex);
                Debugger.Break();
            }
            catch (DbUpdateException ex)
            {
                // Datenbank kann nicht aktualisiert werden
                Console.WriteLine(ex);
                Debugger.Break();
            }
            catch (Exception ex)
            {
                // Etwas anderes ist schief gelaufen. Panik!
                Console.WriteLine(ex);
                Debugger.Break();
            }
            #endregion

            #region  Mehere Entitäten
            for (int i = 0; i < 10; i++)
            {
                // Instanz erzeugen und an den Context anfügen
                Speaker speaker2 = new Speaker();

                speaker2.Name = $"Speaker {i}";
                speaker2.Homepage = "http://www.homepage.eu";

                _efContext.Speakers.Add(speaker2);
            }
            _efContext.SaveChanges();
            #endregion

            #region Parent-Child
            TechEvent te = new TechEvent();
            te.Name = "Demo TechEvent";
            te.Begin = DateTime.Today.AddDays(10);
            te.End = DateTime.Today.AddDays(10);
            te.Price = 99.9m;
            te.Created = DateTime.Now;

            _efContext.Add(te);

            for (int i = 0; i < 10; i++)
            {
                Session session = new Session();
                session.Title = $"Session: {i}";
                session.Difficulty = DifficultyLevel.Level3;
                session.Duration = 60;
                session.SpeakerId = 1;
                session.Begin = DateTime.Now;
                session.End = DateTime.Now.AddMinutes(60);
                session.Abstract = "Abstract";

                te.Sessions.Add(session);
            }
            _efContext.SaveChanges();

            #endregion
        }
    }
}