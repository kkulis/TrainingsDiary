﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Data;
using TrainingDiary.Models.ViewModels;

namespace TrainingDiary.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategories();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var categories = _dbContext.Categories;
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return result;
        }
    }
}