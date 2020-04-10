using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingDiary.Models.ViewModels;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IexerciseCollectionService _exerciseCollectionService;
        private readonly ICategoryService _categoryService;
        public ExerciseController(IexerciseCollectionService iexerciseCollectionService, ICategoryService categoryService)
        {
            _exerciseCollectionService = iexerciseCollectionService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> ShowExercises()
        {
            var exercises = await _exerciseCollectionService.GetExercises();

            return View(exercises);
        }

        [HttpGet]
        public async Task<IActionResult> AddExercise()
        {
            var categories = await _categoryService.GetCategories();

            var categoriesSelectList = new SelectList(categories, "Id", "Name");

            return View(new AddExerciseViewModel()
            {
                Categories = categoriesSelectList
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExercise(AddExerciseViewModel addExerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                await _exerciseCollectionService.AddExercise(addExerciseViewModel);
                return RedirectToAction("ShowExercises");
            }

            var categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View(new AddExerciseViewModel()
            {
                Categories = categories
            });

        }

        [HttpGet]
        public async Task<IActionResult> EditExercise(Guid exerciseId)
        {
            var exercise = await _exerciseCollectionService.GetExercise(exerciseId);

            var categories = await _categoryService.GetCategories();

            var categoriesSelectList = new SelectList(categories, "Id", "Name");

            return View(new AddExerciseViewModel()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Categories = categoriesSelectList,
                Category = exercise.Category
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditExercise(AddExerciseViewModel addExerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                await _exerciseCollectionService.EditExercise(addExerciseViewModel);
                return RedirectToAction("ShowExercises");
            }

            var categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View(new AddExerciseViewModel()
            {
                Categories = categories
            });

        }

        [HttpGet]
        public async Task<IActionResult> DeleteExercise(Guid exerciseId)
        {
            var exercise = await _exerciseCollectionService.GetExercise(exerciseId);

            return View(new AddExerciseViewModel()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Category = exercise.Category
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(AddExerciseViewModel addExerciseViewModel)
        {
            var exerciseId = addExerciseViewModel.Id;
            await _exerciseCollectionService.DeleteExercise(exerciseId);
            return RedirectToAction("ShowExercises");
        }

        


    }
}