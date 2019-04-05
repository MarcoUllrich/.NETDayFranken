// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Logging;

namespace dotnetconsulting.ConsoleLogging
{
    public static class ProgramEventId
    {
        public static readonly EventId CriticalSituation201
            = new EventId(201, "CriticalSituation201");

        public static readonly EventId PermitNotFound
            = new EventId(202, "PermitNotFound");

        public static readonly EventId ImportFileFailed
            = new EventId(203, "ImportFileFailed");
    }   
}