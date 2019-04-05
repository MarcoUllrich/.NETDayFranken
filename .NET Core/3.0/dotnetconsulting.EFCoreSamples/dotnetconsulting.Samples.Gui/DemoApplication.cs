// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.EFContext;
using dotnetconsulting.Samples.Gui.DemoJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace dotnetconsulting.Samples.Gui
{
    public class DemoApplication
    {
        private readonly IServiceProvider iocContainer;

        public DemoApplication(IServiceProvider iocContainer)
        {
            this.iocContainer = iocContainer;
        }

        public void Run()
        {
            ILogger logger = iocContainer.GetService<ILogger<DemoApplication>>();
            logger.LogInformation("== Running ==");

            // Data Seeding
            SamplesContext1 efContext = iocContainer.GetService<SamplesContext1>();
            efContext.SeedDemoData();

            // Demos
            IDemoJob demoJob;
            demoJob = iocContainer.GetService<CreateEntities>();
            // demoJob = iocContainer.GetService<QueryEntities>();
            // demoJob = iocContainer.GetService<ModifyEntities>();
            // demoJob = iocContainer.GetService<DeleteEntities>();
            // demoJob = iocContainer.GetService<DbFunction>();
            // demoJob = iocContainer.GetService<ChangeTracker>();
            // demoJob = iocContainer.GetService<DirectSql>();
            // demoJob = iocContainer.GetService<QueryTypes>();
            // demoJob = iocContainer.GetService<Concurrency>();
            // demoJob = iocContainer.GetService<GlobalQueryFilter>();
            // demoJob = iocContainer.GetService<ShadowProperties>();
            // demoJob = iocContainer.GetService<ExplicitlyCompiledQueries>();
            // demoJob = iocContainer.GetService<LoadingStrategies>();
            // demoJob = iocContainer.GetService<GraphUpdate>();
            // demoJob = iocContainer.GetService<Transactions>();

            // Und Action!
            Console.WriteLine($"=== {demoJob.Title} ===");
            demoJob.Run();

            logger.LogInformation("== Fertig ==");
            Console.ReadKey();
        }
    }
}