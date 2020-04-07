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
                var TrainingNumber = await _trainingService.AddTraining(createTrainingViewModel);
                return RedirectToAction("CreateTraining", new { TrainingNumber });
            }

            return View(createTrainingViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTraining(string searchString, int TrainingNumber)
        {
            var exercises = await _exerciseService.GetExercises(searchString);
            var training = await _trainingService.GetTraining(TrainingNumber);

            return View(new CreateTrainingViewModel()
            {
                TrainingNumber = training.TrainingNumber,
                ExerciseViewModels = exercises,
                DoneExerciseViewModels = training.DoneExerciseViewModels
            }) ;  
        }

        [HttpPost]
        public async Task<IActionResult> CreateTraining(CreateTrainingViewModel createTrainingViewModel)
        {
            if (ModelState.IsValid)
            {
                var exerciseId = await _trainingService.AddExercise(createTrainingViewModel);
                return RedirectToAction("AddExercise", new { exerciseId });
            }

            return View(createTrainingViewModel);
        }


        public async Task<IActionResult> AddExercise(Guid? exerciseId)
        {
            var exercise = await _exerciseService.Get1Exercise(exerciseId.Value);

            var trainingId = exercise.TrainingId;

            int trainingNumber = await _trainingService.GetTrainingNumber(trainingId);

            ViewBag.TrainingNumber = trainingNumber;

            return View(exercise);
           
        }

        public async Task<IActionResult> GetExercise(Guid? exerciseId)
        {
            var exercise = await _exerciseService.Get1Exercise(exerciseId.Value);


            return Json(exercise);
        }

        [HttpPost]
        //[Route("CreateTraining/AddExercise/")]
        public async Task<IActionResult> PostExercise([FromBody] ExerciseViewModel exerciseViewModel)
        {
            
            if(ModelState.IsValid)
            {
                var TrainingNumber = await _exerciseService.AddExercise(exerciseViewModel);
                return RedirectToAction("CreateTraining", new { TrainingNumber });
            }
                 
            return View(exerciseViewModel);
            
        }

        [HttpGet]
        public async Task<IActionResult> ExerciseDetails(Guid exerciseId)
        {
            var exercise = await _exerciseService.Get1Exercise(exerciseId);

            var trainingId = exercise.TrainingId;

            int trainingNumber = await _trainingService.GetTrainingNumber(trainingId);

            ViewBag.TrainingNumber = trainingNumber;

            return View(exercise);
        }
    }
}