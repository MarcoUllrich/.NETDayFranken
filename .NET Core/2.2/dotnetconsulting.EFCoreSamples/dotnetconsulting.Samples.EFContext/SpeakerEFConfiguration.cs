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
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnetconsulting.Samples.EFContext
{
    public class SpeakerEFConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);

            builder.HasKey(s => s.Id);
            builder.Property(p => p.Id);

            builder.Property(s => s.Infos)
                    .IsRequired()
                    .HasDefaultValue("(Keine Infos)");

            builder.Property(p => p.Created)
                    .IsRequired()
                    .HasDefaultValueSql("getdate()");

            builder.Property(p => p.Updated)
                    .IsRequired(false)
                    .HasDefaultValue(null);
            // .IsConcurrencyToken();

            builder.Property(p => p.IsDeleted)
                    .IsRequired()
                    .HasDefaultValue(false);

            builder.Property(p => p.Homepage)
                .HasConversion(
                    c => c.ToLower(),
                    c => c
                );
        }
    }
}