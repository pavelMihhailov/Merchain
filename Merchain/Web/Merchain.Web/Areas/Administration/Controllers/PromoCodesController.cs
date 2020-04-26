namespace Merchain.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchain.Services.Data.Interfaces;
    using Merchain.Web.ViewModels.PromoCodes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class PromoCodesController : AdministrationController
    {
        private readonly IPromoCodesService promoCodesService;
        private readonly ILogger<PromoCodesController> logger;

        public PromoCodesController(IPromoCodesService promoCodesService, ILogger<PromoCodesController> logger)
        {
            this.promoCodesService = promoCodesService;
            this.logger = logger;
        }

        public IActionResult Index(int? page = 1)
        {
            var promoCodes = this.promoCodesService.GetAllUnused();

            var pageSize = 15;
            var codesCount = promoCodes.Count();

            this.ViewBag.CurrPage = page;
            this.ViewBag.MaxPage = (codesCount / pageSize) + (codesCount % pageSize == 0 ? 0 : 1);

            return this.View(promoCodes.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList());
        }

        public IActionResult GenerateCodes()
        {
            var viewModel = new PromoCodesGenerateViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCodes(PromoCodesGenerateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.promoCodesService.GenerateNewCodes(
                inputModel.Count,
                inputModel.PercentageDiscount,
                inputModel.ValidUntil);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var promoCode = await this.promoCodesService.GetByIdAsync((int)id);

            if (promoCode == null)
            {
                return this.NotFound();
            }

            return this.View(promoCode);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.promoCodesService.GetByIdAsync(id);

            try
            {
                await this.promoCodesService.Delete(product);

                this.ViewBag.SuccessMessage = "Succesfully Deleted Promo Code.";
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Could not delete promo code.\n-{ex.Message}");

                this.ViewBag.ErrorMessage = "Deleting Promo Code Failed.";
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
