﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingDiary.Data;

namespace TrainingDiary.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrainingDiary.Data.POCO.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"),
                            Name = "Klata"
                        });
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            Id = new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"),
                            CategoryId = new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"),
                            Name = "Wyciskanie sztangi na ławce płaskiej"
                        });
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.ExerciseTraining", b =>
                {
                    b.Property<Guid>("TrainingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("TrainingId", "ExerciseID");

                    b.HasIndex("ExerciseID");

                    b.ToTable("TrainingExercises");

                    b.HasData(
                        new
                        {
                            TrainingId = new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"),
                            ExerciseID = new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"),
                            Reps = 12,
                            Series = 4,
                            Weight = 60f
                        });
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TrainingEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrainingNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TrainingStart")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TrainingTime")
                        .HasColumnType("time");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("TrainingNumber");

                    b.HasIndex("UserId");

                    b.ToTable("Trainings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"),
                            TrainingEnd = new DateTime(2020, 1, 1, 12, 23, 48, 0, DateTimeKind.Unspecified),
                            TrainingNumber = 1,
                            TrainingStart = new DateTime(2020, 1, 1, 11, 23, 44, 0, DateTimeKind.Unspecified),
                            TrainingTime = new TimeSpan(0, 0, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.Exercise", b =>
                {
                    b.HasOne("TrainingDiary.Data.POCO.Category", "Category")
                        .WithMany("Exercises")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.ExerciseTraining", b =>
                {
                    b.HasOne("TrainingDiary.Data.POCO.Exercise", "Exercise")
                        .WithMany("ExerciseTraining")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingDiary.Data.POCO.Training", "Training")
                        .WithMany("ExerciseTraining")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingDiary.Data.POCO.Training", b =>
                {
                    b.HasOne("TrainingDiary.Data.POCO.User", null)
                        .WithMany("Trainings")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
