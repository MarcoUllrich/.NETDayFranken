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
    public class LoadingStrategies : IDemoJob
    {
        private readonly ILogger<LoadingStrategies> _logger;
        private readonly SamplesContext1 _efContext;

        public LoadingStrategies(ILogger<LoadingStrategies> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Loading Strategies";

        public void Run()
        {
            Debugger.Break();

            int speakerId = 10;
            int techeventId = 11;

            // Welche Strategie darf es sein?
            Strategy strategy = Strategy.LazyLoadingAutomatic;

            switch (strategy)
            {
                case Strategy.LazyLoadingAutomatic:
                    #region LazyLoadingAutomatic
                    Console.Clear();
                    // Wird nicht unterstützt
                    break;
                    #endregion
                case Strategy.LazyLoadingExplicit:
                    #region LazyLoadingExplicit
                    Console.Clear();

                    Speaker speaker2 = _efContext.Speakers.Find(speakerId);
                    _efContext.Entry(speaker2).Collection(e => e.SpeakerSessions).Load();

                    TechEvent techEvent = _efContext.TechEvents.Find(techeventId);
                    _efContext.Entry(techEvent).Reference(e => e.VenueSetup).Load();

                    break;
                #endregion
                case Strategy.Preloading:
                    #region Preloading
                    Console.Clear();

                    // Alle Sprecher laden
                    _efContext.Speakers.ToList();

                    // Zugriff aus dem Speicher
                    Speaker speaker3 = _efContext.Speakers.Find(speakerId);

                    break;
                    #endregion
                case Strategy.EagerLoading:
                    #region EagerLoading

                    TechEvent techEvent4 = _efContext.TechEvents
                        .Include(i => i.Sessions).ThenInclude(i => i.SpeakerSessions)
                        .Include(i => i.VenueSetup)
                        .SingleOrDefault(t => t.Id == techeventId);
                    Console.WriteLine(techEvent4);
                    break;
                    #endregion
                default:
                    break;
            }
        }

        public enum Strategy
        {
            LazyLoadingAutomatic,
            LazyLoadingExplicit,
            Preloading,
            EagerLoading
        }
    }
}