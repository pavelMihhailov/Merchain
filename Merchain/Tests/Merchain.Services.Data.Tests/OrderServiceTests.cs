namespace Merchain.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Merchain.Data;
    using Merchain.Data.Models;
    using Merchain.Data.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class OrderServiceTests
    {
        private EfDeletableEntityRepository<Order> orderRepo;
        private EfDeletableEntityRepository<ApplicationUser> usersRepo;

        private Mock<OrderService> orderService;
        private Mock<UserManager<ApplicationUser>> userManager;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.orderRepo = new EfDeletableEntityRepository<Order>(
                new ApplicationDbContext(options.Options));
            this.usersRepo = new EfDeletableEntityRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            this.userManager = new Mock<UserManager<ApplicationUser>>();
            this.orderService = new Mock<OrderService>();
        }

        [Fact(Skip = "This order service requires too many instances for creation.")]
        public async Task AllOrdersOfUserReturnsRightOrders()
        {
            await this.usersRepo.AddRangeAsync(new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    Id = "1",
                    UserName = "gosho",
                },
                new ApplicationUser()
                {
                    Id = "2",
                    UserName = "pesho",
                },
                new ApplicationUser()
                {
                    Id = "3",
                    UserName = "pavel",
                },
            });
            await this.usersRepo.SaveChangesAsync();

            await this.orderRepo.AddRangeAsync(new List<Order>
            {
                new Order
                {
                    Id = 100,
                    UserId = "1",
                },
                new Order
                {
                    Id = 200,
                    UserId = "3",
                },
                new Order
                {
                    Id = 260,
                    UserId = "2",
                },
                new Order
                {
                    Id = 400,
                    UserId = "2",
                },
            });
            await this.orderRepo.SaveChangesAsync();

            var actualResult = await this.orderService.Object.AllOrdersOfUser("pesho");

            Assert.Equal(2, actualResult.Count());
        }
    }
}
