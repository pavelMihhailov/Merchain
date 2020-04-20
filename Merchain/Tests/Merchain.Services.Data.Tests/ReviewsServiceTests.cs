namespace Merchain.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data;
    using Merchain.Data.Models;
    using Merchain.Data.Repositories;
    using Merchain.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class ReviewsServiceTests
    {
        private EfDeletableEntityRepository<Review> reviewRepo;
        private Mock<ILogger<ReviewsService>> logger;

        public ReviewsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.reviewRepo = new EfDeletableEntityRepository<Review>(
                new ApplicationDbContext(options.Options));
            this.logger = new Mock<ILogger<ReviewsService>>();
        }

        [Fact]
        public async Task AddReviewAddItSuccessfully()
        {
            var reviewService = new ReviewsService(this.reviewRepo, this.logger.Object);

            var reviewId = await reviewService.AddReview("1", 2, 4, "Very good product", "Yeah, it's really good");

            Assert.NotEqual(-1, reviewId);
        }

        [Fact]
        public async Task GetReviewsForProductIsGettingRightResults()
        {
            await this.reviewRepo.AddRangeAsync(new List<Review>
            {
                new Review
                {
                    Id = 1,
                    ProductId = 10,
                    Title = "Test 1",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 2,
                    ProductId = 15,
                    Title = "Test 2",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 3,
                    ProductId = 10,
                    Title = "Test 3",
                    CreatedOn = DateTime.UtcNow,
                },
            });
            await this.reviewRepo.SaveChangesAsync();

            var reviewService = new ReviewsService(this.reviewRepo, this.logger.Object);

            AutoMapperConfig.RegisterMappings(typeof(ReviewTestModel).Assembly);

            var reviewsForProductId10 = reviewService.GetReviewsForProduct<ReviewTestModel>(10);
            var reviewsForNonExistingProduct = reviewService.GetReviewsForProduct<ReviewTestModel>(100000);

            Assert.Equal(2, reviewsForProductId10.Count());
            Assert.Empty(reviewsForNonExistingProduct);
        }

        [Fact]
        public async Task GetProductReviewsCountReturnsRightNumber()
        {
            var reviews = new List<Review>();

            for (int i = 1; i <= 50; i++)
            {
                reviews.Add(new Review
                {
                    Id = i,
                    ProductId = 10,
                    Title = $"Test {i}",
                    CreatedOn = DateTime.UtcNow,
                });
            }

            for (int i = 100; i <= 120; i++)
            {
                reviews.Add(new Review
                {
                    Id = i,
                    ProductId = 20,
                    Title = $"Test {i}",
                    CreatedOn = DateTime.UtcNow,
                });
            }

            await this.reviewRepo.AddRangeAsync(reviews);
            await this.reviewRepo.SaveChangesAsync();

            var reviewsService = new ReviewsService(this.reviewRepo, this.logger.Object);
            var reviewsOfProductId10 = reviewsService.GetProductReviewsCount(10);
            var reviewsOfNonExistingProduct = reviewsService.GetProductReviewsCount(9000);

            Assert.Equal(50, reviewsOfProductId10);
            Assert.Equal(0, reviewsOfNonExistingProduct);
        }

        [Fact]
        public async Task AvgProductStarsWorksAsExpected()
        {
            await this.reviewRepo.AddRangeAsync(new List<Review>()
            {
                new Review
                {
                    Id = 1,
                    ProductId = 10,
                    Stars = 4,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 2,
                    ProductId = 10,
                    Stars = 2,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 4,
                    ProductId = 499,
                    Stars = 4,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 6,
                    ProductId = 499,
                    Stars = 5,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },

                new Review
                {
                    Id = 10,
                    ProductId = 50,
                    Stars = 2,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 15,
                    ProductId = 50,
                    Stars = 3,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
                new Review
                {
                    Id = 20,
                    ProductId = 50,
                    Stars = 2,
                    Title = $"Test Title",
                    CreatedOn = DateTime.UtcNow,
                },
            });
            await this.reviewRepo.SaveChangesAsync();

            var reviewsService = new ReviewsService(this.reviewRepo, this.logger.Object);

            var avgStarsOfProductId10 = reviewsService.AvgProductStars(10);
            var avgStarsOfProductId50 = reviewsService.AvgProductStars(50);
            var avgStarsOfProductId499 = reviewsService.AvgProductStars(499);
            var avgStarsOfNonExistingProduct = reviewsService.AvgProductStars(1000000);

            Assert.Equal(3, avgStarsOfProductId10);
            Assert.Equal(2, avgStarsOfProductId50);
            Assert.Equal(5, avgStarsOfProductId499);
            Assert.Equal(0, avgStarsOfNonExistingProduct);
        }

        public class ReviewTestModel : IMapFrom<Review>
        {
            public int Id { get; set; }

            public int ProductId { get; set; }
        }
    }
}
