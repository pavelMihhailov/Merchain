namespace Merchain.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface ICartService
    {
        Task<Task> AddToCart(ISession session, int id);

        void RemoveFromCart(ISession session, int id);
    }
}
