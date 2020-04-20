namespace Merchain.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        private Mock<IDeletableEntityRepository<Category>> mockRepo;
        private EfDeletableEntityRepository<Category> inMemoryRepo;

        public CategoriesServiceTests()
        {
            this.mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            this.mockRepo.Setup(x => x.All()).Returns(new List<Category>
                                                    {
                                                        new Category { Title = "Test 1" },
                                                        new Category { Title = "Test 2" },
                                                        new Category { Title = "Test 3" },
                                                        new Category { Title = "Test 4" },
                                                        new Category { Title = "Test 5" },
                                                    }.AsQueryable());

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.inMemoryRepo = new EfDeletableEntityRepository<Category>(
                new ApplicationDbContext(options.Options));
        }

        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var categoryService = new CategoriesService(this.mockRepo.Object);
            var actualResult = categoryService.GetCount();

            Assert.Equal(5, actualResult);
        }

        [Fact]
        public async Task AddCategoryAsyncIsAddingCorrectly()
        {
            var categoryService = new CategoriesService(this.inMemoryRepo);

            await categoryService.AddCategoryAsync(new Category() { Title = "TestCategory" });

            var actualResult = categoryService.GetAllAsync().GetAwaiter().GetResult()
                .FirstOrDefault(x => x.Title == "TestCategory");

            Assert.NotNull(actualResult);
        }

        [Fact]
        public async Task GetByIdAsyncReturnsRightId()
        {
            await this.inMemoryRepo.AddAsync(new Category() { Id = 1, Title = "Test Id" });
            await this.inMemoryRepo.SaveChangesAsync();

            var categoryService = new CategoriesService(this.inMemoryRepo);
            var category = await categoryService.GetByIdAsync(1);

            Assert.NotNull(category);
        }
    }
}
