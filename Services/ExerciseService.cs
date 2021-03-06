﻿using AutoMapper;
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
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ExerciseService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;

        }

        public async Task<IList<ExerciseViewModel>> GetExercises(string searchString, string userId)
        {
            var exercises = _applicationDbContext.Exercises
                .Where(e => e.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(e => e.Name.Contains(searchString));
            }

            var exerciseList = exercises.Include(el => el.Category).ToList();

            var exercisesVm = _mapper.Map<List<ExerciseViewModel>>(exerciseList);
            return exercisesVm;
        }

        public async Task<ExerciseViewModel> Get1Exercise(Guid? exerciseId)
        {
            var exercise = await _applicationDbContext.TrainingExercises.Include(e => e.Exercise)
                                                                            .ThenInclude(e => e.Category)
                                                                        .Include(s => s.Series)
                                                                        .FirstOrDefaultAsync(e => e.Id == exerciseId); 
            return _mapper.Map<ExerciseViewModel>(exercise);
        }

        public async Task<int> AddExercise(ExerciseViewModel exerciseViewModel)
        {
            var exerciseId = exerciseViewModel.Id;
            var trainingGuid = exerciseViewModel.TrainingId;

            var training = await _applicationDbContext.Trainings.FirstOrDefaultAsync(t => t.Id == trainingGuid);

            var exercise = await _applicationDbContext.TrainingExercises.FirstOrDefaultAsync(e => e.Id == exerciseId);

            ICollection<Series> series = new List<Series>();

            var seriesDone = exerciseViewModel.SeriesViewModels.ToList();

            foreach (var serie in seriesDone)
            {
                series.Add(new Series()
                {
                    ExerciseTrainingId = exerciseId,
                    Id = Guid.NewGuid(),
                    Reps = serie.Reps,
                    Weight = serie.Weight
                });
            }

            _applicationDbContext.Series.AddRange(series);
            await _applicationDbContext.SaveChangesAsync();


            int trainingNumber = training.TrainingNumber;

            return trainingNumber;

        }

        public async Task<Guid> DeleteExercise(Guid exerciseId)
        {
            var exercise = await _applicationDbContext.TrainingExercises.FirstOrDefaultAsync(e => e.Id == exerciseId);

            _applicationDbContext.TrainingExercises.Remove(exercise);
            await _applicationDbContext.SaveChangesAsync();

            return exercise.TrainingId;
        }


    }

    public interface IExerciseService
    {
        Task<IList<ExerciseViewModel>> GetExercises(string searchString, string userId);
        Task<ExerciseViewModel> Get1Exercise(Guid? exerciseId);
        Task<int> AddExercise(ExerciseViewModel exerciseViewModel);
        Task<Guid> DeleteExercise(Guid exerciseId);
    }
}
