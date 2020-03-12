using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
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

     //   public async Task<ExerciseViewModel>PostExercise(ExerciseViewModel exerciseViewModel)
     //   {

     //   }

    }

    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseViewModel>> GetExercises();
        Task<ExerciseViewModel> Get1Exercise(Guid? exerciseId);
       // Task<ExerciseViewModel> PostExercise(ExerciseViewModel exerciseViewModel);
    }
}
