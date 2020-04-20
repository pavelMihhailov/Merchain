namespace Merchain.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data;
    using Merchain.Data.Models;
    using Merchain.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrderItemServiceTests
    {
        private EfRepository<OrderItem> inMemoryRepo;

        public OrderItemServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.inMemoryRepo = new EfRepository<OrderItem>(
                new ApplicationDbContext(options.Options));
        }

        [Fact]
        public async Task AddOrderItemsAsyncIsAddingSuccessfully()
        {
            var orderItemService = new OrderItemService(this.inMemoryRepo);

            await orderItemService.AddOrderItemsAsync(new List<OrderItem>
            {
                new OrderItem(),
                new OrderItem(),
                new OrderItem(),
                new OrderItem(),
            });

            var actualResult = await this.inMemoryRepo.All().CountAsync();

            Assert.Equal(4, actualResult);
        }

        [Fact]
        public async Task GetProductOrdersCountReturnsRightNumber()
        {
            await this.inMemoryRepo.AddRangeAsync(new List<OrderItem>
            {
                new OrderItem
                {
                    ProductId = 2,
                    Quantity = 5,
                },
                new OrderItem
                {
                    ProductId = 2,
                    Quantity = 2,
                },
                new OrderItem
                {
                    ProductId = 9,
                    Quantity = 4,
                },
            });
            await this.inMemoryRepo.SaveChangesAsync();

            var orderItemService = new OrderItemService(this.inMemoryRepo);

            var actualResult = orderItemService.GetProductOrdersCount(2);

            Assert.Equal(7, actualResult);
        }

        [Fact]
        public async Task GetProductOrdersCountReturnsZeroWhenThereIsNoProduct()
        {
            await this.inMemoryRepo.AddRangeAsync(new List<OrderItem>
            {
                new OrderItem
                {
                    ProductId = 2,
                    Quantity = 5,
                },
                new OrderItem
                {
                    ProductId = 4,
                    Quantity = 2,
                },
            });
            await this.inMemoryRepo.SaveChangesAsync();

            var orderItemService = new OrderItemService(this.inMemoryRepo);

            var actualResult = orderItemService.GetProductOrdersCount(100);

            Assert.Equal(0, actualResult);
        }
    }
}
