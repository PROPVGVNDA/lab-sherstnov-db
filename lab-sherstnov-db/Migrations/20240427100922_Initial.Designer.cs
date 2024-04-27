﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using lab_sherstnov_db.Models;

#nullable disable

namespace lab_sherstnov_db.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20240427100922_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_catalog", "adminpack");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("lab_sherstnov_db.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ClassTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("class_time");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<int?>("MaximumParticipants")
                        .HasColumnType("integer")
                        .HasColumnName("maximum_participants");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TrainerId")
                        .HasColumnType("integer")
                        .HasColumnName("trainer_id");

                    b.HasKey("Id")
                        .HasName("classes_pkey");

                    b.HasIndex("TrainerId");

                    b.ToTable("classes", (string)null);
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.ClassRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("integer")
                        .HasColumnName("class_id");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer")
                        .HasColumnName("member_id");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("date")
                        .HasColumnName("registration_date");

                    b.HasKey("Id")
                        .HasName("classregistrations_pkey");

                    b.HasIndex("ClassId");

                    b.HasIndex("MemberId");

                    b.ToTable("classregistrations", (string)null);
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<DateOnly>("DateJoined")
                        .HasColumnType("date")
                        .HasColumnName("date_joined");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("members_pkey");

                    b.HasIndex(new[] { "Email" }, "members_email_key")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "members_phone_key")
                        .IsUnique();

                    b.ToTable("members", (string)null);
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.MemberTrainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer")
                        .HasColumnName("member_id");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<int>("TrainerId")
                        .HasColumnType("integer")
                        .HasColumnName("trainer_id");

                    b.HasKey("Id")
                        .HasName("membertrainer_pkey");

                    b.HasIndex("MemberId");

                    b.HasIndex("TrainerId");

                    b.ToTable("membertrainer", (string)null);
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("phone");

                    b.Property<string>("Specialization")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("specialization");

                    b.HasKey("Id")
                        .HasName("trainers_pkey");

                    b.HasIndex(new[] { "Email" }, "trainers_email_key")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "trainers_phone_key")
                        .IsUnique();

                    b.ToTable("trainers", (string)null);
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Class", b =>
                {
                    b.HasOne("lab_sherstnov_db.Models.Trainer", "Trainer")
                        .WithMany("Classes")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("classes_trainer_id_fkey");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.ClassRegistration", b =>
                {
                    b.HasOne("lab_sherstnov_db.Models.Class", "Class")
                        .WithMany("ClassRegistrations")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("classregistrations_class_id_fkey");

                    b.HasOne("lab_sherstnov_db.Models.Member", "Member")
                        .WithMany("ClassRegistrations")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("classregistrations_member_id_fkey");

                    b.Navigation("Class");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.MemberTrainer", b =>
                {
                    b.HasOne("lab_sherstnov_db.Models.Member", "Member")
                        .WithMany("MemberTrainers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("membertrainer_member_id_fkey");

                    b.HasOne("lab_sherstnov_db.Models.Trainer", "Trainer")
                        .WithMany("MemberTrainers")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("membertrainer_trainer_id_fkey");

                    b.Navigation("Member");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Class", b =>
                {
                    b.Navigation("ClassRegistrations");
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Member", b =>
                {
                    b.Navigation("ClassRegistrations");

                    b.Navigation("MemberTrainers");
                });

            modelBuilder.Entity("lab_sherstnov_db.Models.Trainer", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("MemberTrainers");
                });
#pragma warning restore 612, 618
        }
    }
}
