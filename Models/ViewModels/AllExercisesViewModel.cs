using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class AllExercisesViewModel
    {
        public IEnumerable<ExerciseCollectionViewModel> ExerciseViewModels  { get; set; }
    }
}
