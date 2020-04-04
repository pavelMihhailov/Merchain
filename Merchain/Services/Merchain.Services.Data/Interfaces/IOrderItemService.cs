namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();

        IEnumerable<T> GetAll<T>();

        Task AddOrderItemsAsync(IEnumerable<OrderItem> orderItems);
    }
}
