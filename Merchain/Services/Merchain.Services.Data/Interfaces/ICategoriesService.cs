namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
