namespace Merchain.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Merchain.Common;
    using Merchain.Services.Messaging;
    using Merchain.Web.ViewModels.Email;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class InfoController : BaseController
    {
        private readonly SendGridEmailSender emailSender;
        private readonly ILogger<InfoController> logger;

        public InfoController(SendGridEmailSender emailSender, ILogger<InfoController> logger)
        {
            this.emailSender = emailSender;
            this.logger = logger;
        }

        [ResponseCache(Duration = 60 * 60 * 24 * 3, Location = ResponseCacheLocation.Any)]
        public IActionResult PrivacyPolicy()
        {
            return this.View();
        }

        public IActionResult ContactUs()
        {
            this.HandlePopupMessages();

            return this.View(new EmailInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailToUs(EmailInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "Невалидна изпратена форма, моля опитайте пак.";

                return this.RedirectToAction("ContactUs");
            }

            try
            {
                await this.emailSender.SendEmailAsync(
                    inputModel.Email,
                    inputModel.Name,
                    GlobalConstants.CompanyEmail,
                    inputModel.Subject,
                    inputModel.Content);

                this.TempData[ViewDataConstants.SucccessMessage] = "Вашият имейл е изпратен успешно.";
            }
            catch (Exception ex)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "Възникна проблем с изпращането на имейла, моля опитайте по-късно.";
                this.logger.LogError($"Could not submit email.\n--{ex.Message}");
            }

            return this.RedirectToAction("ContactUs");
        }
    }
}
