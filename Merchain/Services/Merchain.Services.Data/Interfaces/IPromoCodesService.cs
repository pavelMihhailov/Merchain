namespace Merchain.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public interface IPromoCodesService
    {
        Task<PromoCode> GetByIdAsync(int id);

        IEnumerable<T> GetAllOfUserId<T>(string userId);

        IEnumerable<PromoCode> GetAll();

        IEnumerable<PromoCode> GetAllUnused();

        Task<Task> Delete(PromoCode promoCode);

        Task GenerateNewCodes(int count, int percentageDiscount, DateTime validUntil);
    }
}
