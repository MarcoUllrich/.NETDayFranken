using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dotnetconsulting.Samples.EFDatabaseFirst
{
    public partial class AdventureWorksDW2014Context : DbContext
    {
        public virtual DbSet<DatabaseLog> DatabaseLog { get; set; }
        public virtual DbSet<DimScenario> DimScenario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.;Database=AdventureWorksDW2014;Trusted_Connection=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseLog>(entity =>
            {
                entity.Property(e => e.DatabaseLogId).HasColumnName("DatabaseLogID");

                entity.Property(e => e.DatabaseUser)
                    .IsRequired()
                    .HasColumnType("sysname")
                    .HasMaxLength(4000);

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasColumnType("sysname")
                    .HasMaxLength(4000);

                entity.Property(e => e.Object)
                    .HasColumnType("sysname")
                    .HasMaxLength(4000);

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.Property(e => e.Schema)
                    .HasColumnType("sysname")
                    .HasMaxLength(4000);

                entity.Property(e => e.Tsql)
                    .IsRequired()
                    .HasColumnName("TSQL");

                entity.Property(e => e.XmlEvent)
                    .IsRequired()
                    .HasColumnType("xml");
            });

            modelBuilder.Entity<DimScenario>(entity =>
            {
                entity.HasKey(e => e.ScenarioKey);

                entity.Property(e => e.ScenarioName).HasMaxLength(50);
            });
        }
    }
}
