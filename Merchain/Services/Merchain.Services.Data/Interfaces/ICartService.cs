namespace Merchain.Services.Data.Interfaces
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICartService
    {
        Task<Task> AddToCart(ISession session, int id, int quantity, string size, int? colorId);

        void RemoveFromCart(ISession session, int id, string size, int? colorId);

        HttpStatusCode DecreaseQuantity(ISession session, int id, string size, int? colorId);

        void EmptyCart(ISession session);

        int GetCartItemsCount(ISession session);
    }
}
