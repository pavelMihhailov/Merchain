namespace Merchain.Web.Controllers
{
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.PromoCodes;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PromoCodesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPromoCodesService promoCodesService;

        public PromoCodesController(
            UserManager<ApplicationUser> userManager,
            IPromoCodesService promoCodesService)
        {
            this.userManager = userManager;
            this.promoCodesService = promoCodesService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            var myPromoCodes = this.promoCodesService.GetAllOfUserId<MyPromoCodesViewModel>(user.Id);

            return this.View(myPromoCodes);
        }
    }
}
