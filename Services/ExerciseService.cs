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
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
  
        public ExerciseService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<ExerciseViewModel>> GetExercises()
        {
            var exercises = _applicationDbContext.Exercises.Include(e => e.Category).ToList();
            var exercisesVm = _mapper.Map<List<ExerciseViewModel>>(exercises);
            return exercisesVm;
        }

        public async Task<ExerciseViewModel> Get1Exercise(Guid? exerciseId)
        {
            var exercise = await _applicationDbContext.Exercises.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == exerciseId);
            return _mapper.Map<ExerciseViewModel>(exercise);
        }

        public async Task<ExerciseTraining>AddExercise(ExerciseViewModel exerciseViewModel)
        {
            var exercise = new ExerciseTraining
            {
                ExerciseID = exerciseViewModel.Id,
            };


            ICollection<Series> series = new List<Series>();

            var exerciseSeries = exerciseViewModel.SeriesViewModels.ToList();
            foreach (var SeriesViewModel in exerciseSeries)
            {
                series.Add(new Series()
                {
                    Reps = SeriesViewModel.Reps,
                    Weight = SeriesViewModel.Weight
                }); 
            }

            exercise.Series = series;

            var addResult = _applicationDbContext.TrainingExercises.AddAsync(exercise);
            await _applicationDbContext.SaveChangesAsync();
            return (await addResult).Entity;
        }

    }

    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseViewModel>> GetExercises();
        Task<ExerciseViewModel> Get1Exercise(Guid? exerciseId);
        Task<ExerciseTraining> AddExercise(ExerciseViewModel exerciseViewModel);
    }
}
