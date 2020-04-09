using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingDiary.Models.ViewModels
{
    public class AllTrainingsViewModel
    {
        public int TrainingsNumber { get; set; }
        public IEnumerable<TrainingSummaryViewModel> Trainings { get; set; }
    }
}
