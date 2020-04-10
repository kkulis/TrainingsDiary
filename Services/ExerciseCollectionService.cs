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
    public interface IexerciseCollectionService
    {
        public Task<AllExercisesViewModel> GetExercises();
        public Task AddExercise(AddExerciseViewModel addExerciseViewModel);
    }
    public class ExerciseCollectionService :IexerciseCollectionService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public ExerciseCollectionService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<AllExercisesViewModel> GetExercises()
        {
            var exercises =  _applicationDbContext.Exercises.Include(e => e.Category).ToList();

            var exercisesMapped = _mapper.Map <IEnumerable<ExerciseCollectionViewModel>>(exercises);

            return new AllExercisesViewModel
            {
                ExerciseViewModels = exercisesMapped
            };

        }

        public async Task AddExercise(AddExerciseViewModel addExerciseViewModel)
        {
            Guid exerciseId = Guid.NewGuid();

            var exercise = new Exercise
            {
                Id = exerciseId,
                Name = addExerciseViewModel.Name,
                CategoryId = addExerciseViewModel.Category.Id
            };

            _applicationDbContext.Exercises.Add(exercise);
            await _applicationDbContext.SaveChangesAsync();
        }

    }
}
