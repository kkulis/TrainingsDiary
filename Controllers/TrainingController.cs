using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [ValidateAntiForgeryToken]
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
                TrainigTime = training.TrainigTime,
                ExerciseViewModels = exercises,
                DoneExerciseViewModels = training.DoneExerciseViewModels
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTraining(CreateTrainingAddExerciseViewModel createTrainingAddExerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                var exerciseId = await _trainingService.AddExercise(createTrainingAddExerciseViewModel);
                return RedirectToAction("AddExercise", new { exerciseId });
            }

            return View(createTrainingAddExerciseViewModel);
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

            if (ModelState.IsValid)
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

        [HttpGet]
        public async Task<IActionResult> DeleteExercise(Guid? exerciseId)
        {
            var exercise = await _exerciseService.Get1Exercise(exerciseId);

            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(ExerciseViewModel exerciseViewModel)
        {
            var exerciseId = exerciseViewModel.Id;

            var trainingId = await _exerciseService.DeleteExercise(exerciseId);

            int trainingNumber = await _trainingService.GetTrainingNumber(trainingId);

            TempData["Message"] = "Exercise Removed";
            return RedirectToAction("CreateTraining", new { trainingNumber });
        }
    }
}