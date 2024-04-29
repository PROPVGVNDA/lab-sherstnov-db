using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab_sherstnov_db.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassRegistration> Classregistrations { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberTrainer> Membertrainers { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            //.UseLazyLoadingProxies()
            .UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=1425;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classes_pkey");

            entity.ToTable("classes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("class_time");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MaximumParticipants).HasColumnName("maximum_participants");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("classes_trainer_id_fkey");
        });

        modelBuilder.Entity<ClassRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classregistrations_pkey");

            entity.ToTable("classregistrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassRegistrations)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("classregistrations_class_id_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.ClassRegistrations)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("classregistrations_member_id_fkey");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("members_pkey");

            entity.ToTable("members");

            entity.HasIndex(e => e.Email, "members_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "members_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DateJoined).HasColumnName("date_joined");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<MemberTrainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("membertrainer_pkey");

            entity.ToTable("membertrainer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberTrainers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("membertrainer_member_id_fkey");

            entity.HasOne(d => d.Trainer).WithMany(p => p.MemberTrainers)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("membertrainer_trainer_id_fkey");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainers_pkey");

            entity.ToTable("trainers");

            entity.HasIndex(e => e.Email, "trainers_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "trainers_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .HasColumnName("specialization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
