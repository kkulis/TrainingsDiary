using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class ExerciseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public int Series { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
    }
}
