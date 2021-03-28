namespace Merchain.Web.Controllers
{
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Data.Models;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Messaging;
    using Merchain.Web.ViewModels.Subscribe;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SubscribeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SendGridEmailSender emailSender;
        private readonly IPromoCodesService promoCodesService;

        public SubscribeController(
            UserManager<ApplicationUser> userManager,
            SendGridEmailSender emailSender,
            IPromoCodesService promoCodesService)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.promoCodesService = promoCodesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new SubscribeViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            user.IsSubscribed = true;

            await this.userManager.UpdateAsync(user);

            var assignedPromoCode = await this.promoCodesService.AssignCode(user.Id);

            await this.emailSender.SendEmailAsync(
                    GlobalConstants.CompanyEmail,
                    "BG Coding Life",
                    email,
                    "Твоят промо код",
                    $"Твоят промо код е: {assignedPromoCode.Code} и е валиден до {assignedPromoCode.ValidUntil.ToLocalTime()}");

            this.TempData[ViewDataConstants.SucccessMessage] = "Успешно се абонирахте за нашия седмичен бюлетин. Вашият промо код е добавен в профила Ви.";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
