using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class TrainingSummaryViewModel
    {
        public int TrainingNumber { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
        public TimeSpan TrainigTime { get; set; }
        public IList<ExerciseViewModel> DoneExerciseViewModels { get; set; }
    }
}
