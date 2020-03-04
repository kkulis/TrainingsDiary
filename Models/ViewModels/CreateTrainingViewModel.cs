using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class CreateTrainingViewModel
    {
        public int TrainingNumber { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
        public IList<ExerciseViewModel> ExerciseViewModels { get; set; }
    }
}
