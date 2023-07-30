﻿using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.ViewModels.Category;
using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly HustlerzOasizDbContext data;

        public CategoryService(HustlerzOasizDbContext data) => this.data = data;

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await data.Categories.AnyAsync(c => c.Id == id);
            return result;
        }

        public async Task<IEnumerable<ChooseACategoryFormModel>> GetCategoriesAsync()
        {
            var categories = await data.Categories.Select(c => new ChooseACategoryFormModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToArrayAsync();

            return categories;
        }
    }
}