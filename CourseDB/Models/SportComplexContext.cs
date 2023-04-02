using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseDB.Models;

public partial class SportComplexContext : DbContext
{
    public SportComplexContext()
    {
    }

    public SportComplexContext(DbContextOptions<SportComplexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClubCard> ClubCards { get; set; }

    public virtual DbSet<ClubCardService> ClubCardServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<VisitMode> VisitModes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Sport-complex;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Adress).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
        });

        modelBuilder.Entity<ClubCard>(entity =>
        {
            entity.ToTable("ClubCard");

            entity.Property(e => e.ClubCardId)
                .ValueGeneratedNever()
                .HasColumnName("ClubCardID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.VisitModeId).HasColumnName("VisitModeID");

            entity.HasOne(d => d.Client).WithMany(p => p.ClubCards)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ClubCard_Client");

            entity.HasOne(d => d.VisitMode).WithMany(p => p.ClubCards)
                .HasForeignKey(d => d.VisitModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClubCard_VisitMode");
        });

        modelBuilder.Entity<ClubCardService>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ClubCardService");

            entity.Property(e => e.ClubCardId).HasColumnName("ClubCardID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.ClubCard).WithMany()
                .HasForeignKey(d => d.ClubCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClubCardService_ClubCard");

            entity.HasOne(d => d.Service).WithMany()
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClubCardService_Service");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(50);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.ToTable("Visit");

            entity.Property(e => e.VisitId)
                .ValueGeneratedNever()
                .HasColumnName("VisitID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Client).WithMany(p => p.Visits)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visit_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.Visits)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Visit_Service");
        });

        modelBuilder.Entity<VisitMode>(entity =>
        {
            entity.ToTable("VisitMode");

            entity.Property(e => e.VisitModeId)
                .ValueGeneratedNever()
                .HasColumnName("VisitModeID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
