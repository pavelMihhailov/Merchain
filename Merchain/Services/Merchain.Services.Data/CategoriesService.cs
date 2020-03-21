namespace Merchain.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public int GetCount()
        {
            return this.categoryRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoryRepository.All().To<T>().ToList();
        }
    }
}
