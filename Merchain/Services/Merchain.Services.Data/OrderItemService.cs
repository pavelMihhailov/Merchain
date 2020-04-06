namespace Merchain.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> orderItemRepo;

        public OrderItemService(IRepository<OrderItem> orderItemRepo)
        {
            this.orderItemRepo = orderItemRepo;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await this.orderItemRepo.All().ToListAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.orderItemRepo.All().To<T>().ToList();
        }

        public async Task AddOrderItemsAsync(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                return;
            }

            await this.orderItemRepo.AddRangeAsync(orderItems);
            await this.orderItemRepo.SaveChangesAsync();

            return;
        }

        public int GetProductOrdersCount(int productId)
        {
            if (productId <= 0)
            {
                return 0;
            }

            var productOrdersCount = this.orderItemRepo.All()
                .Where(x => x.ProductId == productId)
                .Sum(p => p.Quantity);

            return productOrdersCount;
        }
    }
}
