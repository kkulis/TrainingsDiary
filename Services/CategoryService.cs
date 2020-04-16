using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
using TrainingDiary.Data.POCO;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Services
{
    public interface ICategoryService
    {
        public Task<IList<CategoryViewModel>> GetCategories(string userId);
        public Task AddCategory(CategoryViewModel categoryViewModel, string userId);
        public Task<CategoryViewModel> Get1Category(Guid categoryId);
        public Task EditCategory(AddCategoryViewModel categoryViewModel);
        public Task DeleteCategory(Guid categoryId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddCategory(CategoryViewModel categoryViewModel, string userId)
        {
            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = categoryViewModel.Name,
                UserId = userId
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCategory(AddCategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _dbContext.Entry(await _dbContext.Categories.FirstAsync(e => e.Id == categoryViewModel.Id)).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CategoryViewModel> Get1Category(Guid categoryId)
        {
            var category = await  _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<IList<CategoryViewModel>> GetCategories(string userId)
        {
            var categories = _dbContext.Categories
                .Where(c => c.UserId == userId)
                .ToList();
            var result = _mapper.Map<IList<CategoryViewModel>>(categories);
            return result;
        }

       
    }
}
