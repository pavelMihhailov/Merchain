﻿namespace Merchain.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICartService
    {
        Task<Task> AddToCart(ISession session, int id, int quantity);

        void RemoveFromCart(ISession session, int id);

        void EmptyCart(ISession session);

        int GetCartItemsCount(ISession session);
    }
}
