using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class Series
    {
        public Guid Id { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public Guid ExerciseTrainingId { get; set; }
        public virtual ExerciseTraining ExerciseTraining { get; set; }
    }
}
