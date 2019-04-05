// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.EFContext;
using Microsoft.Extensions.Logging;
using System;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class DemoJob1 : IDemoJob
    {
        private readonly ILogger<DemoJob1> _logger;
        private readonly SamplesContext1 _efContext;

        public DemoJob1(ILogger<DemoJob1> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "DemoJob1";

        public void Run()
        {

        }
    }
}