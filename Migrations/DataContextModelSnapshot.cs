﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QMSL;

#nullable disable

namespace QMSL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DoctorPatient", b =>
                {
                    b.Property<int>("DoctorsId")
                        .HasColumnType("int");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("DoctorsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("DoctorPatient", (string)null);
                });

            modelBuilder.Entity("GeneralAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GeneralQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralQuestionId");

                    b.ToTable("GeneralAnswers", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CommentedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("EditablePollId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EditablePollId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fathername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.EditableAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EditableQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EditableQuestionId");

                    b.ToTable("EditableAnswers", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.EditablePoll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PassedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("EditablePolls", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.EditableQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChoosenAnswer")
                        .HasColumnType("int");

                    b.Property<int>("EditablePollId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EditablePollId");

                    b.ToTable("EditableQuestions", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.GeneralPoll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("GeneralPolls", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.GeneralQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GeneralPollId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralPollId");

                    b.ToTable("GeneralQuestions", (string)null);
                });

            modelBuilder.Entity("QMSL.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Disease")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fathername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("DoctorPatient", b =>
                {
                    b.HasOne("QMSL.Models.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QMSL.Models.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeneralAnswer", b =>
                {
                    b.HasOne("QMSL.Models.GeneralQuestion", null)
                        .WithMany("GeneralAnswers")
                        .HasForeignKey("GeneralQuestionId");
                });

            modelBuilder.Entity("QMSL.Models.Comment", b =>
                {
                    b.HasOne("QMSL.Models.EditablePoll", null)
                        .WithMany("Comments")
                        .HasForeignKey("EditablePollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QMSL.Models.EditableAnswer", b =>
                {
                    b.HasOne("QMSL.Models.EditableQuestion", null)
                        .WithMany("EditableAnswers")
                        .HasForeignKey("EditableQuestionId");
                });

            modelBuilder.Entity("QMSL.Models.EditablePoll", b =>
                {
                    b.HasOne("QMSL.Models.Patient", null)
                        .WithMany("Polls")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QMSL.Models.EditableQuestion", b =>
                {
                    b.HasOne("QMSL.Models.EditablePoll", null)
                        .WithMany("Questions")
                        .HasForeignKey("EditablePollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QMSL.Models.GeneralPoll", b =>
                {
                    b.HasOne("QMSL.Models.Doctor", null)
                        .WithMany("Polls")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QMSL.Models.GeneralQuestion", b =>
                {
                    b.HasOne("QMSL.Models.GeneralPoll", null)
                        .WithMany("Questions")
                        .HasForeignKey("GeneralPollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QMSL.Models.Doctor", b =>
                {
                    b.Navigation("Polls");
                });

            modelBuilder.Entity("QMSL.Models.EditablePoll", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QMSL.Models.EditableQuestion", b =>
                {
                    b.Navigation("EditableAnswers");
                });

            modelBuilder.Entity("QMSL.Models.GeneralPoll", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QMSL.Models.GeneralQuestion", b =>
                {
                    b.Navigation("GeneralAnswers");
                });

            modelBuilder.Entity("QMSL.Models.Patient", b =>
                {
                    b.Navigation("Polls");
                });
#pragma warning restore 612, 618
        }
    }
}
