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
using Microsoft.Extensions.Logging;
using System;

namespace dotnetconsulting.Samples.EFContext
{
    public class SamplesContext1 : DbContext
    {
        public SamplesContext1()
        {
        }

        public SamplesContext1(DbContextOptions<SamplesContext1> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            if (!optionsBuilder.IsConfigured)
            {
                // Für UnitTests, die nicht konfigurieren
                // Oder für update-database
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                const string CONSTRING = @"Server=(localdb)\mssqllocaldb;Database=dotnetconsulting.EFCoreSamples;Trusted_Connection=True;MultipleActiveResultSets=True;";
                const string CONSTRINGSQLAZURE = @"Server=tcp:dncserver.database.windows.net,1433;Initial Catalog=dncdatabase;Persist Security Info=False;User ID=tkansy;Password=Passwo0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
#pragma warning restore CS0219 // Variable is assigned but its value is never used

                optionsBuilder.UseSqlServer(CONSTRING);
            }
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dnc");
            //modelBuilder.HasDbFunction(GetType()
            //                .GetMethod(nameof(SamplesContext1.StringLike)),
            //                o =>
            //                {
            //                    o.HasName("StringLike");
            //                    o.HasSchema("dbo");
            //                });

            //modelBuilder.ForSqlServerUseSequenceHiLo();
            //modelBuilder.ForSqlServerUseIdentityColumns();

            #region Session
            // Tabelle-/ Schemaname in Datenbank
            modelBuilder.Entity<Session>()
                .ToTable("Sessions");

            // Eindeutiger Schlüssel der Entität
            modelBuilder.Entity<Session>()
                .HasKey(k => k.Id);

            // Spaltename in Datenbank
            modelBuilder.Entity<Session>()
                .Property(p => p.Abstract)
                .HasColumnName("ContentDescription");

            // Index auf Spalten
            modelBuilder.Entity<Session>()
                .HasIndex(i => i.Title);

            // Feld ein Pflichtfeld?
            modelBuilder.Entity<Session>()
                .Property(p => p.Abstract)
                .IsRequired();

            // Feld eine Unicode-Zeichenkette?
            modelBuilder.Entity<Session>()
                .Property(p => p.Abstract)
                .IsRequired();

            // Maximale Länge
            modelBuilder.Entity<Session>()
                .Property(p => p.Abstract)
                .HasMaxLength(300);

            // Datentypen für Spalte festlegen
            modelBuilder.Entity<Session>()
                .Property(p => p.Begin);
                // .HasColumnType("time");

            modelBuilder.Entity<Session>()
                .Property(p => p.Created)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Session>()
                .Property(p => p.Updated)
                .IsRequired(false)
                .HasDefaultValue(null)
                .IsConcurrencyToken();

            modelBuilder.Entity<Session>()
                .Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Session>()
                .HasQueryFilter(p => p.IsDeleted == false);
            #endregion

            #region Speaker
            modelBuilder.ApplyConfiguration(new SpeakerEFConfiguration());
            //modelBuilder.Entity<Speaker>(
            //    e =>
            //    {
            //        e.Property(p => p.Id).HasColumnName("Id");
            //        e.Property(p => p.Name);
            //    }
            //    );
            #endregion

            #region TechEvent
            modelBuilder.Entity<TechEvent>()
                .Property<String>("Code")
                .HasColumnName("SecretCode");
            #endregion

            modelBuilder.Entity<Session>()
                .HasIndex(p => new { p.Created, p.Updated })
                .IsUnique(true);

            //modelBuilder.Entity<Session>()
            //    .Property(p => p.Duration)
            //    .ValueGeneratedNever()
            //    // oder
            //    .ValueGeneratedOnAddOrUpdate()
            //    // oder
            //    .ValueGeneratedOnAdd()
            //    // oder
            //    .ValueGeneratedOnUpdate();

            //modelBuilder.Entity<Session>()
            //    .Property(s => s.Begin)
            //    .IsRequired()
            //    .HasColumnName("BeginTime")
            //    .HasColumnType("time");

            //modelBuilder.Entity<Session>()
            //    .Property(s => s.End)
            //    .IsRequired()
            //    .HasColumnName("EndTime")
            //    .HasColumnType("time");

            //modelBuilder.Entity<Session>()
            //    .Property(s => s.Duration)
            //    .HasComputedColumnSql("DATEDIFF(Second, [BeginTime], [EndTime]) / 60");

            //modelBuilder.Entity<Session>()
            //    .Property(p => p.Title)
            //    .HasField("_SessionTitle")
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);

            //    modelBuilder.Entity<Session>()
            //        .HasOne(p => p.TechEvent)
            //        .WithMany(p => p.Sessions)
            //        .HasConstraintName("MyFKConstraint")
            //        .HasForeignKey(p => p.TechEventId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<TechEvent>()
            //    .HasMany(p => p.Sessions)
            //    .WithOne(p => p.TechEvent)
            //    .HasConstraintName("MyFKConstraint")
            //    .HasForeignKey(p => p.TechEventId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TechEvent>()
                .HasOne(p => p.VenueSetup)
                .WithOne(p => p.TechEvent)
                .HasForeignKey<VenueSetup>(f => f.TechEventId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // Query Type
            modelBuilder
                .Query<SpeakerStatistics>().ToView("vwSpeakerStats")
                .Property(v => v.NumerOfSessions).HasColumnName("SessionCount");

            base.OnModelCreating(modelBuilder);
        }

        [DbFunction("StringLike", "dbo")]
        public static bool StringLike(string @string, string Pattern)
        {
            throw new Exception("No direct Call");
        }

        public override int SaveChanges()
        {
            if (!ChangeTracker.AutoDetectChangesEnabled)
                ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }

        public DbSet<TechEvent> TechEvents { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerSession> SpeakerSessions { get; set; }
        public DbQuery<SpeakerStatistics> SpeakerStatistics { get; set; }
    }

    public static class DbContextExtensions
    {
        public static void LogToConsole(this DbContext context)
        {
            ILoggerFactory loggerFactory = context.GetService<ILoggerFactory>();
            loggerFactory.AddConsole();
        }
    }
}