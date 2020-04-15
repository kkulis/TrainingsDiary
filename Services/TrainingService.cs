using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly TimeCalculator _timeCalculator;
        private readonly IMapper _mapper;

        public TrainingService(ApplicationDbContext applicationDbContext, TimeCalculator timeCalculator, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _timeCalculator = timeCalculator;
            _mapper = mapper;
        }

        public async Task<int> AddTraining(CreateTrainingViewModel createTrainingViewModel, string userId)
        {
            var trainingId = Guid.NewGuid();
            
            var training = new Training
            {
                UserId = userId,
                Id = trainingId,
                TrainingStart = createTrainingViewModel.TrainingStart,
                TrainingEnd = createTrainingViewModel.TrainingEnd,
                TrainingTime = await _timeCalculator.GetTrainingTime(createTrainingViewModel)
            };

            var trainingFromDb = _applicationDbContext.Trainings.Add(training);
            await _applicationDbContext.SaveChangesAsync();

            return trainingFromDb.Entity.TrainingNumber;
        }

        public async Task<Guid> AddExercise(CreateTrainingAddExerciseViewModel createTrainingAddExerciseViewModel)
        {
            int trainingNumber = createTrainingAddExerciseViewModel.trainingNumber;

            var training = await _applicationDbContext.Trainings.Include(t => t.ExerciseTraining).FirstOrDefaultAsync(t => t.TrainingNumber == trainingNumber);

            var trainingGuid = training.Id;

            var exerciseId = Guid.NewGuid();

            var exerciseFromVm = createTrainingAddExerciseViewModel.ExerciseId;

            var exercise = new ExerciseTraining
            {
                Id = exerciseId,
                ExerciseID = exerciseFromVm,
                TrainingId = trainingGuid,
            };

            _applicationDbContext.TrainingExercises.Add(exercise);
            await _applicationDbContext.SaveChangesAsync();

            return exerciseId;


        }

        public async Task<CreateTrainingViewModel> GetTraining(int trainingId)
        {
            var training = await _applicationDbContext.Trainings.Include(t => t.ExerciseTraining)
                                                                .ThenInclude(et => et.Series)
                                                                .Include(et => et.ExerciseTraining)
                                                                .ThenInclude(s => s.Exercise)
                                                                .ThenInclude(c => c.Category)
                                                                .FirstOrDefaultAsync(t => t.TrainingNumber == trainingId);

            return _mapper.Map<CreateTrainingViewModel>(training);
        }

        public async Task<int> GetTrainingNumber(Guid trainingId)
        {
            var training = await _applicationDbContext.Trainings.FirstOrDefaultAsync(t => t.Id == trainingId);

            return training.TrainingNumber;
        }

        public async Task DeleteTraining(int trainingNumber)
        {
            var training = await _applicationDbContext.Trainings.FirstOrDefaultAsync(t => t.TrainingNumber == trainingNumber);

            _applicationDbContext.Trainings.Remove(training);
            await _applicationDbContext.SaveChangesAsync();
        }

    }
    public interface ITrainingService
    {
        //Task<int> CreateTraining(CreateTrainingViewModel createTrainingViewModel);
        Task<int> AddTraining(CreateTrainingViewModel createTrainingView, string userId);
        Task<Guid> AddExercise(CreateTrainingAddExerciseViewModel createTrainingAddExerciseViewModel);
        Task<CreateTrainingViewModel> GetTraining(int trainingId);
        Task<int> GetTrainingNumber(Guid trainingId);
        Task DeleteTraining(int trainingNumber);

    }
}
