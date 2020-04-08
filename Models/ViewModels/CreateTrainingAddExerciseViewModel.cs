using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class CreateTrainingAddExerciseViewModel
    {
        public int trainingNumber { get; set; }
        public Guid ExerciseId { get; set; }
    }
}
