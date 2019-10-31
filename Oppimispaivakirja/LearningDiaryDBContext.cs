using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Oppimispaivakirja
{
    partial class LearningDiaryDBContext : DbContext
    {
        public LearningDiaryDBContext()
        {
        }

        public LearningDiaryDBContext(DbContextOptions<LearningDiaryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=LearningDiary;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.Title)
                    .HasColumnName("Title")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(255);

                entity.Property(e => e.EstimatedTimeToMaster)
                    .HasColumnName("TimeToMaster")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.TimeSpent)
                    .HasColumnName("TimeSpent")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Source)
                    .HasColumnName("Source")
                    .HasMaxLength(255);

                entity.Property(e => e.StartLearningDate)
                    .HasColumnName("StartLearningDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.InProgress)
                    .HasColumnName("InProgress")
                    .HasColumnType("bit");

                entity.Property(e => e.CompletionDate)
                    .HasColumnName("CompletionDate")
                    .HasColumnType("datetime");

                //entity.Property(e => e.EstimatedTimeToMaster).HasColumnName("TimeToMaster");
                
                //entity.HasOne(d => d.Asiakas)
                //    .WithMany(p => p.Tilaus)
                //    .HasForeignKey(d => d.AsiakasId)
                //    .HasConstraintName("FK_Tilaus_Asiakas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
