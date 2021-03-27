namespace Merchain.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Merchain.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Display(Name = "Акаунт")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            this.Username = await this.userManager.GetUserNameAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);

            return this.Page();
        }
    }
}
