using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data.POCO;

namespace TrainingDiary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) { }


        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExerciseTraining> TrainingExercises { get; set;}
        public DbSet<Training> Trainings { get; set; }
        public DbSet<User> Users { get; set;  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Training>()
                .Property(t => t.TrainingStart)
                .HasColumnType("datetime2");
            builder.Entity<Training>()
                .Property(t => t.TrainingEnd)
                .HasColumnType("datetime2");
            builder.Entity<Training>()
                .HasAlternateKey(t => t.TrainingNumber);
            builder.Entity<Training>()
                .Property(t => t.TrainingNumber)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Entity<ExerciseTraining>()
                .HasKey(et => new { et.TrainingId, et.ExerciseID });
            builder.Entity<ExerciseTraining>()
                .HasOne(et => et.Exercise)
                .WithMany(e => e.ExerciseTraining)
                .HasForeignKey(et => et.ExerciseID);
            builder.Entity<ExerciseTraining>()
                .HasOne(et => et.Training)
                .WithMany(t => t.ExerciseTraining)
                .HasForeignKey(et => et.TrainingId);

            


            builder.Entity<Category>()
                .HasData(new List<Category>()
                {
                    new Category()
                    {
                        Id = Guid.Parse("63CFBA80-9041-4994-BBC9-9F0F28B51388"),
                        Name = "Klata"
                        
                    },
                    new Category()
                    {
                        Id = Guid.Parse("CF36D573-0160-4252-AB78-B12805AE9C07"),
                        Name = "Plecy"

                    },
                    new Category()
                    {
                        Id = Guid.Parse("F52C961D-BD06-4E33-9ADF-67F587CCAABE"),
                        Name = "Nogi"

                    },

                });

            builder.Entity<Exercise>()
                .HasData(new List<Exercise>()
                {
                   new Exercise()
                   {
                       Id = Guid.Parse("38B381C8-FD1F-408C-AD25-6401FD6F40CA"),
                       Name = "Wyciskanie sztangi na ławce płaskiej",
                       CategoryId = Guid.Parse("63CFBA80-9041-4994-BBC9-9F0F28B51388")
                  
                   },
                   new Exercise()
                   {
                       Id = Guid.Parse("12F1974D-8EBC-4CAA-8D34-7D350B7AF440"),
                       Name = "Wyciskanie hantli na ławce płaskiej",
                       CategoryId = Guid.Parse("63CFBA80-9041-4994-BBC9-9F0F28B51388")

                   },
                   new Exercise()
                   {
                       Id = Guid.Parse("170B139A-B929-40DE-9644-C590B0507819"),
                       Name = "Podciąganie",
                       CategoryId = Guid.Parse("CF36D573-0160-4252-AB78-B12805AE9C07")

                   },
                   new Exercise()
                   {
                       Id = Guid.Parse("D4AD5722-7A0B-4A7A-976D-2772E5DAA0B2"),
                       Name = "przyciąganie wyciągu do klatki",
                       CategoryId = Guid.Parse("CF36D573-0160-4252-AB78-B12805AE9C07")

                   },
                   new Exercise()
                   {
                       Id = Guid.Parse("C7F43E99-E859-43CE-B6AD-3DCAC667A729"),
                       Name = "Przysiad",
                       CategoryId = Guid.Parse("F52C961D-BD06-4E33-9ADF-67F587CCAABE")

                   }


                });

            builder.Entity<ExerciseTraining>()
                .HasData(new List<ExerciseTraining>()
                {
                    new ExerciseTraining()
                    {
                        ExerciseID = Guid.Parse("38B381C8-FD1F-408C-AD25-6401FD6F40CA"),
                        TrainingId = Guid.Parse("6BE892A5-12CA-493D-BB74-4EF5B9175BF5"),
                        Series = 4,
                        Weight = 60,
                        Reps = 12
                    }
                });

            builder.Entity<Training>()
                .HasData(new List<Training>()
                {
                    new Training()
                    {
                        Id = Guid.Parse("6BE892A5-12CA-493D-BB74-4EF5B9175BF5"),
                        TrainingStart = new DateTime(2020, 1, 1, 11,23,44),
                        TrainingEnd = new DateTime(2020, 1, 1, 12,23,48),
                        TrainingNumber = 1
                    }
                });
        }

    }


}
