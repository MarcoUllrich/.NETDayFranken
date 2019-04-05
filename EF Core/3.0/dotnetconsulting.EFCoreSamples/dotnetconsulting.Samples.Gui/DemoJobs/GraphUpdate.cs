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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class GraphUpdate : IDemoJob
    {
        private readonly ILogger<GraphUpdate> _logger;
        private readonly SamplesContext1 _efContext;

        public GraphUpdate(ILogger<GraphUpdate> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Graph Update (aka TrackGraph)";

        public void Run()
        {
            Debugger.Break();

            // Die die eingebttete Ressource ein JSON-Dokument erzeugen
            // string techEventJson = constructTechEventJson();

            // Entität aus Json konstrieren
            TechEvent techEvent = getTechEventFromJson();

            // Verbessert seit EF Core 2.0
            // _efContext.Attach(techEvent);

            // oder

            // Entität durchlaufen rekursive
            _efContext.ChangeTracker.TrackGraph(techEvent, node =>
            {
                // node: EntityEntryGraphNode
                EntityEntry entry = node.Entry;
                Console.WriteLine(entry.Entity.GetType().ToString());

                PropertyEntry propertyEntry = entry.Property("Id");

                // Entscheiden, was neu, was gelöscht und was modifiziert ist
                if ((int)propertyEntry.CurrentValue == 0)
                {
                    // Anlegen
                    entry.State = EntityState.Added;
                    propertyEntry.IsTemporary = true;
                }
                else if ((int)propertyEntry.CurrentValue < 0)
                {
                    // Löschen
                    entry.State = EntityState.Unchanged;
                    _efContext.Entry(cloneObjectWithId(entry.Entity, -((int)propertyEntry.CurrentValue))).State
                        = EntityState.Deleted;
                }
                else
                    // Änderng
                    entry.State = EntityState.Modified;
            });

            // Speichern
            _efContext.SaveChanges();
        }

        private object cloneObjectWithId(object o, int id)
        {
            // Reflection in Action
            object result = Activator.CreateInstance(o.GetType());

            PropertyInfo prop = result.GetType()
                .GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

            if (null != prop && prop.CanWrite)
                prop.SetValue(result, id, null);

            return result;
        }

        private TechEvent getTechEventFromJson()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            const string JSONFILENAME = "dotnetconsulting.Samples.Gui.DemoJobs.Resources.TechEvents.json";

            using (Stream stream = assembly.GetManifestResourceStream(JSONFILENAME))
            using (StreamReader reader = new StreamReader(stream))
                return JsonConvert.DeserializeObject<TechEvent>(reader.ReadToEnd());
        }

        /// <summary>
        /// Zu Testzwecken ein Object erstellen.
        /// </summary>
        /// <returns></returns>
        private string constructTechEventJson()
        {
            int id = -1;

            // Ereigbnis an sich
            TechEvent techEvent = new TechEvent()
            {
                Id = id--,
                Name = "Happy Event",
                Begin = DateTime.Now,
                End = DateTime.Now.AddDays(3),
                ImageUrl = "http://www.dotnetconsulting.eu",
                Created = DateTime.Now,
                Updated = null
            };

            // Veranstaltungsort
            techEvent.VenueSetup.Id = id--;
            techEvent.VenueSetup.Description = "Bayerisches Thema";
            techEvent.Created = DateTime.Now;

            // Und die Session
            for (int i = 0; i < 3; i++)
            {
                Session session = new Session()
                {
                    Id = id--,
                    Title = $"Session {i}",
                    Abstract = "Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    Begin = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Created = DateTime.Now,
                    Updated = null
                };

                techEvent.Sessions.Add(session);
            }

            return JsonConvert.SerializeObject(techEvent, Formatting.Indented);
        }
    }
}