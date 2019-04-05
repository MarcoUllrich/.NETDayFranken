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
    public class QueryEntities : IDemoJob
    {
        private readonly ILogger<QueryEntities> _logger;
        private readonly SamplesContext1 _efContext;

        public QueryEntities(ILogger<QueryEntities> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Query Entities";

        public void Run()
        {
            Debugger.Break();

            Session s1 = _efContext.Sessions.Find(1);

            // Abfrage definieren
            Console.Clear();
            var query1 = from q in _efContext.Speakers.TagWith("Erste Demo-Abfrage")
                         where q.Name.StartsWith("Speaker 2")
                         select q;

            var query2 = _efContext.Speakers.Where(q => q.Name == "Speaker 2").TagWith("Zwei Demo-Abfrage");
            
            var query3 = _efContext.Speakers.Where(w => EF.Functions.Like(w.Name, "Speaker [1-3]"));

            // Ergebnis durchlaufen
            foreach (var item in query1)
                Console.WriteLine(item);
            // Und gleich nochmal (Achtung! Logging Verzögerung)
            foreach (var item in query1)
                Console.WriteLine(item);
            // Und noch einmal
            foreach (var item in query1)
                Console.WriteLine(item);
            //return;
            #region Abfrage mit Projektion
            Console.Clear();

            var query4 = (from q in _efContext.Speakers
                          where q.Name.StartsWith("Speaker 1")
                          select new { q._Id, q.Name, q.Homepage }).First();

            Console.WriteLine(query4);
            #endregion

            #region Abfrage mit Variable
            Console.Clear();

            string speakerName = "Speaker 3";
            Speaker speakerByName = _efContext.Speakers.Where(w => w.Name == speakerName).First();
            #endregion

            #region Abfrage via Find()
            Console.Clear();
            Speaker speaker;
            speaker = (Speaker)_efContext.Find(typeof(Speaker), 1);
            // oder
            speaker = _efContext.Find<Speaker>(1);
            // oder
            speaker = _efContext.Speakers.Find(1);
            #endregion

            #region Nur den Cache verwenden
            Console.Clear();
            speaker = _efContext.Speakers.Local.SingleOrDefault(w => w._Id == 25);
            // Cache und Server
            // RelationalEventId.QueryClientEvaluationWarning sorgt für eine Exception
            speaker = _efContext.Speakers.SingleOrDefault(w => w._Id == 25);
            // Wieder nur Cache
            speaker = _efContext.Speakers.Local.SingleOrDefault(w => w._Id == 25);
            #endregion
        }
    }
}