namespace Merchain.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await this.categoryRepository.All().ToListAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoryRepository.All().To<T>().ToList();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await this.categoryRepository.GetById(id);
        }

        public int GetCount()
        {
            return this.categoryRepository.All().Count();
        }

        public async Task<Task> AddCategoryAsync(Category category)
        {
            category.CreatedOn = DateTime.UtcNow;

            await this.categoryRepository.AddAsync(category);

            await this.categoryRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> Edit(Category category)
        {
            this.categoryRepository.Update(category);

            await this.categoryRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> Delete(Category category)
        {
            this.categoryRepository.Delete(category);

            await this.categoryRepository.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
