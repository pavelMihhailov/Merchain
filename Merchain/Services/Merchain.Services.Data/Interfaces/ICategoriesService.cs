namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public interface ICategoriesService
    {
        int GetCount();

        Task<IEnumerable<Category>> GetAllAsync();

        IEnumerable<T> GetAll<T>();

        Task<Category> GetByIdAsync(int id);

        Task<Task> AddCategoryAsync(Category category);

        Task<Task> Edit(Category category);

        Task<Task> Delete(Category category);
    }
}
