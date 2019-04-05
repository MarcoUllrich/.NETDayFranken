// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace dotnetconsulting.Samples.EFSimpleStandalone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Optionen erzeugen
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlite(@"Data Source=c:\temp\Storage.db");
            //optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.ConfigureWarnings(s =>
            {
                // s.Throw(RelationalEventId.QueryClientEvaluationWarning);
                // Oder
                // s.Log(RelationalEventId.QueryClientEvaluationWarning);
                // Oder
                s.Ignore(RelationalEventId.QueryClientEvaluationWarning);
            });

            // install-package Microsoft.EntityFrameworkCore.Sqlite
            using (MyDbContext context = new MyDbContext(optionsBuilder.Options))
            {
                // Logging
                context.AddConsoleLogging();

                // Neue Instanz erzeugen
                MyEntity myEntity = new MyEntity
                {
                    ValueA = "Walle walle mache Strecke",
                    ValueB = 99
                };

                // Anfügen
                context.Add(myEntity);

                // Speichern
                context.SaveChanges();
            }

            Console.Clear();
            using (MyDbContext context = new MyDbContext(optionsBuilder.Options))
            {
                // Logging
                context.AddConsoleLogging();

                var query1 = (from q in context.MyEntity
                                where q.ValueA.IndexOf("Strecke") > 0
                                orderby q.Id
                                select q).First();

                Console.WriteLine(query1);

                var query2 = context
                            .MyEntity
                            .Where(w => w.ValueA.IndexOf("Strecke") > 0)
                            .OrderBy(o => o.Id)
                            .Select(s => new { s.ValueA, s.ValueB })
                            .First();

                Console.WriteLine(query2);
            }


            Console.WriteLine("== Fertig ==");
            Console.ReadKey();
        }
    }

    public static class DbContextExtensions
    {
        public static void AddConsoleLogging(this DbContext context)
        {
            ILoggerFactory loggerFactory = context.GetService<ILoggerFactory>();

            // install-package Microsoft.Extensions.Logging.Console 
            loggerFactory.AddConsole();
        }
    }
}