namespace Merchain.Web.Controllers
{
    using System.Threading.Tasks;

    using Merchain.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class EcontController : BaseController
    {
        private readonly IEcontService econtService;

        public EcontController(IEcontService econtService)
        {
            this.econtService = econtService;
        }

        public async Task<IActionResult> ValidateAddress(string city, string address, string otherAddress)
        {
            bool addressValid = await this.econtService.ValidateAddress(city, address, otherAddress);

            return this.Json(new { addressValid });
        }
    }
}
