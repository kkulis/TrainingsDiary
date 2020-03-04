using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Data.POCO
{
    public class ExerciseTraining
    {
        public int Series { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public virtual Exercise Exercise { get; set; }
        public Guid ExerciseID { get; set; }
        public virtual Training Training { get; set; }
        public Guid TrainingId { get; set; }

    }
}
