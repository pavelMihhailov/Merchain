namespace Merchain.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public int GetCount()
        {
            return this.productsRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.productsRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllDescending<T>(Expression<Func<Product, object>> orderBy)
        {
            return this.productsRepository.All()
                .OrderByDescending(orderBy)
                .To<T>()
                .ToList();
        }
    }
}
