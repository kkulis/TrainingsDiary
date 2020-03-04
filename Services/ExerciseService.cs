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

        public async Task<ExerciseViewModel> GetExercises()
        {
            var exercises = await _applicationDbContext.Exercises.ToListAsync();
            var exercisesVm = _mapper.Map<ExerciseViewModel>(exercises);
            return exercisesVm;
        }

    }

    public interface IExerciseService
    {
        Task<ExerciseViewModel> GetExercises();
    }
}
