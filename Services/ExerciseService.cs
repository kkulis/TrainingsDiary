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

    }

    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseViewModel>> GetExercises();
    }
}
