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

namespace dotnetconsulting.Samples.EFContext
{
    public class SamplesContext3 : DbContext
    {
        public SamplesContext3(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Speaker> Speakers { get; set; }
    }
}