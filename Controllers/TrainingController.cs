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
        public async Task<IActionResult> AddTraining()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTraining(CreateTrainingViewModel createTrainingViewModel)
        {
            if (ModelState.IsValid)
            {
                var trainingGuid = await _trainingService.AddTraining(createTrainingViewModel);
                return RedirectToAction("CreateTraining", new { trainingGuid });
            }

            return View(createTrainingViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTraining(string searchString)
        {
            var exercises = await _exerciseService.GetExercises(searchString);

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

        [HttpGet]
        public async Task<IActionResult> AddExercise(Guid? exerciseId)
        {
            if (exerciseId == null)
            {
                return RedirectToAction("/CreateTraining");
            }
            var exercise = await _exerciseService.Get1Exercise(exerciseId.Value);


            return View(exercise);
           
        }

        [HttpPost]
        //[Route("CreateTraining/AddExercise/")]
        public async Task<IActionResult> AddExercise(ExerciseViewModel exerciseViewModel)
        {
            if(ModelState.IsValid)
            {
                await _exerciseService.AddExercise(exerciseViewModel);
                return RedirectToAction("/CreateTraining");
            }
                 
            return View(exerciseViewModel);
            
        }
    }
}