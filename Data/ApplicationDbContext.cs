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
                        
                    }
                });

            builder.Entity<Exercise>()
                .HasData(new List<Exercise>()
                {
                   new Exercise()
                   {
                       Id = Guid.Parse("38B381C8-FD1F-408C-AD25-6401FD6F40CA"),
                       Name = "Wyciskanie sztangi na ławce płaskiej",
                       CategoryId = Guid.Parse("63CFBA80-9041-4994-BBC9-9F0F28B51388")
                  
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
