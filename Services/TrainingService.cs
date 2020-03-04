using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
using TrainingDiary.Data.POCO;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TimeCalculator _timeCalculator;

        public TrainingService(ApplicationDbContext dbContext, TimeCalculator timeCalculator)
        {
            _dbContext = dbContext;
            _timeCalculator = timeCalculator;
        }

        public async Task<int> CreateTraining(CreateTrainingViewModel createTrainingViewModel)
        {
            var trainingId = Guid.NewGuid();

            var training = new Training
            {
                Id = trainingId,
                TrainingStart = createTrainingViewModel.TrainingStart,
                TrainingEnd = createTrainingViewModel.TrainingEnd,
                TrainingTime = await _timeCalculator.GetTrainingTime(createTrainingViewModel)
            };

            ICollection<ExerciseTraining> exerciseTrainings = new List<ExerciseTraining>();

            var exerciseTraining = createTrainingViewModel.ExerciseViewModels.Where(evm => evm.Series != 0);
            foreach (var exerciseViewModel in exerciseTraining)
            {
                exerciseTrainings.Add(new ExerciseTraining()
                {
                    TrainingId = trainingId,
                    ExerciseID = exerciseViewModel.Id,
                    Series = exerciseViewModel.Series,
                    Reps = exerciseViewModel.Reps,
                    Weight = exerciseViewModel.Weight

                });
            }

            training.ExerciseTraining = exerciseTrainings;

            var orderFromDb = _dbContext.Trainings.Add(training);
            await _dbContext.SaveChangesAsync();

            return orderFromDb.Entity.TrainingNumber;
        }

    }
    public interface ITrainingService
    {
        Task<int> CreateTraining(CreateTrainingViewModel createTrainingViewModel);
    }
}
