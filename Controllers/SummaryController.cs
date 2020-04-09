using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingDiary.Models.ViewModels;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ISummaryService _summaryService;
        private readonly IExerciseService _exerciseService;
        private readonly ITrainingService _trainingService;
        public SummaryController(ISummaryService summaryService, IExerciseService exerciseService, ITrainingService trainingService)
        {
            _summaryService = summaryService;
            _exerciseService = exerciseService;
            _trainingService = trainingService;
        }
        [HttpGet]
        public async Task<IActionResult> TrainingSummary(int trainingNumber)
        {
            var training = await _summaryService.GetDoneTraining(trainingNumber);
            return View(training);
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
        public async Task<IActionResult> AllTrainings()
        {
            var trainings = await _summaryService.GetAllTrainings();
            return View(trainings);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTraining(int trainingNumber)
        {
            var training = await _summaryService.GetDoneTraining(trainingNumber);
            return View(training);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTraining(TrainingSummaryViewModel trainingSummaryViewModel)
        {
            var trainingNumber = trainingSummaryViewModel.TrainingNumber;

            await _trainingService.DeleteTraining(trainingNumber);

            TempData["Message"] = "Training Removed";
            return RedirectToAction("AllTrainings");
        }


    }
}