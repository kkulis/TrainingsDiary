using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingDiary.Models.ViewModels;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetCategories(userId);

            var vm = new AllCategoriesViewModel
            {
                Categories = categories
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(categoryViewModel, userId);
                return RedirectToAction("ShowCategories");
            }

            return View(categoryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid categoryId)
        {
            var category = await _categoryService.Get1Category(categoryId);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult>EditCategory(AddCategoryViewModel categoryViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            categoryViewModel.UserId = userId;

            await _categoryService.EditCategory(categoryViewModel);
            return RedirectToAction("ShowCategories");
        }

        [HttpPost]
        public async Task<IActionResult>DeleteCategory(Guid categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);

            return RedirectToAction("ShowCategories");

        }
    }
}