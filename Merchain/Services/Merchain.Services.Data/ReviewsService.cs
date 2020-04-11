namespace Merchain.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Web.ViewModels.Reviews;
    using Microsoft.Extensions.Logging;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepo;
        private readonly ILogger<ReviewsService> logger;

        public ReviewsService(IDeletableEntityRepository<Review> reviewRepo, ILogger<ReviewsService> logger)
        {
            this.reviewRepo = reviewRepo;
            this.logger = logger;
        }

        public async Task<int> AddReview(string fromUserId, int forProductId, int stars, string title, string content)
        {
            try
            {
                var review = new Review()
                {
                    UserId = fromUserId,
                    ProductId = forProductId,
                    Stars = stars,
                    Title = title,
                    Content = content,
                };

                await this.reviewRepo.AddAsync(review);
                await this.reviewRepo.SaveChangesAsync();

                return review.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User could not create a review for product ID: {forProductId}.", ex.Message);

                return -1;
            }
        }

        public IEnumerable<T> GetReviewsForProduct<T>(int productId)
        {
            var reviews = this.reviewRepo.All()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>().ToList();

            return reviews;
        }

        public int GetProductReviewsCount(int productId)
        {
            var reviewsCount = this.reviewRepo.All()
                .Where(x => x.ProductId == productId)
                .Count();

            return reviewsCount;
        }

        public int AvgProductStars(int productId)
        {
            var productReviews = this.reviewRepo.All()
                .Where(x => x.ProductId == productId)
                .ToList();

            var avgStars = 0;

            if (productReviews.Count() > 0)
            {
                avgStars = (int)Math.Floor(productReviews.Average(x => x.Stars));
            }

            return avgStars;
        }
    }
}
