namespace Merchain.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Merchain.Data.Models;

    public interface IPromoCodesService
    {
        Task<PromoCode> GetByIdAsync(int id);

        PromoCode GetByCodeAsync(string forUserId, string code);

        IEnumerable<T> GetAllOfUserId<T>(string userId);

        IEnumerable<PromoCode> GetAll();

        IEnumerable<PromoCode> GetAllUnused();

        Task<Task> Delete(PromoCode promoCode);

        Task<PromoCode> Edit(PromoCode promoCode);

        Task GenerateNewCodes(int count, int percentageDiscount, DateTime validUntil);

        Task<PromoCode> AssignCode(string toUserId);

        Task MarkAsUsed(int id);
    }
}
