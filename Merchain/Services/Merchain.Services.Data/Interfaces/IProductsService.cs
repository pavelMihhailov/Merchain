namespace Merchain.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IProductsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllDescending<T>(Expression<Func<Product, object>> orderBy);

        Task<Product> GetByIdAsync(int id);

        T GetById<T>(int id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<Task> AddProductAsync(Product product);

        Task CreateProduct(
            string name,
            string description,
            decimal price,
            IEnumerable<IFormFile> images,
            IEnumerable<int> categoryIds);

        Task<Task> Edit(Product product, IEnumerable<IFormFile> addedImages, IEnumerable<int> categoryIds);

        Task<Task> Delete(Product product);

        IEnumerable<Product> GetProductsByCategory(int? categoryId);

        IEnumerable<Product> GetProductsByCategory(string categoryName);

        Task AddProductToWishList(ISession session, int id);

        void RemoveProductFromWishList(ISession session, int id);
    }
}
