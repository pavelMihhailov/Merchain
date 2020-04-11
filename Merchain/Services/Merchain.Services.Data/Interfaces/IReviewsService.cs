namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<int> AddReview(string fromUserId, int forProductId, int stars, string title, string content);

        IEnumerable<T> GetReviewsForProduct<T>(int productId);

        int GetProductReviewsCount(int productId);

        int AvgProductStars(int productId);
    }
}
