using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingDiary.Models.ViewModels;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IExerciseService _exerciseService;

        public TrainingController(ITrainingService trainingService, IExerciseService exerciseService)
        {
            _trainingService = trainingService;
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateTraining()
        {
            var exercises = await _exerciseService.GetExercises();
            return View(new CreateTrainingViewModel()
            {
                ExerciseViewModels = exercises

            });

        }

        [HttpPost]
        public async Task<IActionResult> CreateTraining(CreateTrainingViewModel createTrainingViewModel)
        {
            if (ModelState.IsValid)
            {
                await _trainingService.CreateTraining(createTrainingViewModel);
            }

            return View(createTrainingViewModel);
        }

    }
}