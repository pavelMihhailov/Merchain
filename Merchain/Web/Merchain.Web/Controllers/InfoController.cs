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

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailToUs(EmailInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "Sorry your submitted form was not valid.";

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

                this.TempData[ViewDataConstants.SucccessMessage] = "Your email was submitted successfuly.";
            }
            catch (Exception ex)
            {
                this.TempData[ViewDataConstants.ErrorMessage] = "Sorry, there was a problem submiting the email.";
                this.logger.LogError($"Could not submit email.\n--{ex.Message}");
            }

            return this.RedirectToAction("ContactUs");
        }
    }
}
