using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Services
{
    public class TimeCalculator
    {
        public async Task<TimeSpan> GetTrainingTime(CreateTrainingViewModel createTrainingViewModel)
        {
            return createTrainingViewModel.TrainingEnd - createTrainingViewModel.TrainingStart;
        }

    }

}
