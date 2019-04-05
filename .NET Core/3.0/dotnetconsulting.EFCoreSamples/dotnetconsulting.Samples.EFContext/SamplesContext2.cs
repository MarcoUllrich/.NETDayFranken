// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace dotnetconsulting.Samples.EFContext
{
    public class SamplesContext2 : DbContext
    {
        public bool IncludeSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheFactory>();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!IncludeSessions)
                modelBuilder.Entity<Speaker>().Ignore(p => p.SpeakerSessions);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Speaker> Speakers { get; set; }
    }

    public class DynamicModelCacheFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            if (context is SamplesContext2 sampleContext2)
            {
                return (context.GetType(), sampleContext2.IncludeSessions);
            }
            else
                return context.GetType();
        }
    }
}