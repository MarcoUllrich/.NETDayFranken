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
    public class Session
    {
        public int Id { get; set; }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Abstract { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public int Duration { get; set; }

        public int? EventId { get; set; }

        public ICollection<SpeakerSession> SpeakerSessions { get; set; }

        public int SpeakerId { get; set; }

        public TechEvent TechEvent { get; set; }

        public int TechEventId { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }

        public Session()
        {
            SpeakerSessions = new HashSet<SpeakerSession>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public enum DifficultyLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4
    }
}