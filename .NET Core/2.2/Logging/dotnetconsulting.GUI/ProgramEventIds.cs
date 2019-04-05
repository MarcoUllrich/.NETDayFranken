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