namespace Merchain.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Merchain.Data.Models;

    public interface IProductsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllDescending<T>(Expression<Func<Product, object>> orderBy);
    }
}
