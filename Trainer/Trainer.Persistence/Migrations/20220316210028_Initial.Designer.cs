﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trainer.Persistence;

#nullable disable

namespace Trainer.Persistence.Migrations
{
    [DbContext(typeof(TrainerDbContext))]
    [Migration("20220316210028_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Trainer.Domain.Entities.BaseUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Email")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("LastName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BaseUsers", "user");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Examination.Examination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Indicators")
                        .HasColumnType("int");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TypePhysicalActive")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.OTP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OTPs");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Patient.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Hobbies")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients", "patient");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Result.Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AverageDia")
                        .HasColumnType("int");

                    b.Property<int>("AverageHeartRate")
                        .HasColumnType("int");

                    b.Property<int>("AverageOxigen")
                        .HasColumnType("int");

                    b.Property<int>("AverageSis")
                        .HasColumnType("int");

                    b.Property<double>("AverageTemperature")
                        .HasColumnType("float");

                    b.Property<Guid?>("ExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationId")
                        .IsUnique()
                        .HasFilter("[ExaminationId] IS NOT NULL");

                    b.HasIndex("PatientId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Admin.Admin", b =>
                {
                    b.HasBaseType("Trainer.Domain.Entities.BaseUser");

                    b.ToTable("Admins", "user");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Doctor.Doctor", b =>
                {
                    b.HasBaseType("Trainer.Domain.Entities.BaseUser");

                    b.ToTable("Doctors", "user");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Manager.Manager", b =>
                {
                    b.HasBaseType("Trainer.Domain.Entities.BaseUser");

                    b.ToTable("Managers", "user");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Examination.Examination", b =>
                {
                    b.HasOne("Trainer.Domain.Entities.Patient.Patient", "Patient")
                        .WithMany("Examinations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Result.Result", b =>
                {
                    b.HasOne("Trainer.Domain.Entities.Examination.Examination", "Examination")
                        .WithOne("Result")
                        .HasForeignKey("Trainer.Domain.Entities.Result.Result", "ExaminationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Trainer.Domain.Entities.Patient.Patient", "Patient")
                        .WithMany("Results")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Examination");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Admin.Admin", b =>
                {
                    b.HasOne("Trainer.Domain.Entities.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("Trainer.Domain.Entities.Admin.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Doctor.Doctor", b =>
                {
                    b.HasOne("Trainer.Domain.Entities.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("Trainer.Domain.Entities.Doctor.Doctor", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Manager.Manager", b =>
                {
                    b.HasOne("Trainer.Domain.Entities.BaseUser", null)
                        .WithOne()
                        .HasForeignKey("Trainer.Domain.Entities.Manager.Manager", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Examination.Examination", b =>
                {
                    b.Navigation("Result")
                        .IsRequired();
                });

            modelBuilder.Entity("Trainer.Domain.Entities.Patient.Patient", b =>
                {
                    b.Navigation("Examinations");

                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
