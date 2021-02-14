namespace Merchain.Services.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Services.Econt.Models.Response;

    public interface IEcontService
    {
        Task<IQueryable<Office>> GetOffices();
    }
}
