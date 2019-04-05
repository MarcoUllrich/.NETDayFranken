// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dotnetconsulting.Samples.Domains
{
    public class TechEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Venue { get; set; }

        public VenueSetup VenueSetup { get; set; }

        public string WebSite { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        public TechEvent()
        {
            Sessions = new HashSet<Session>();
            VenueSetup = new VenueSetup();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}