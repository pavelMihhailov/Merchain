namespace Merchain.Web.Controllers
{
    using Merchain.Common;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected void HandlePopupMessages()
        {
            this.ViewData[ViewDataConstants.SucccessMessage] = this.TempData[ViewDataConstants.SucccessMessage];
            this.ViewData[ViewDataConstants.ErrorMessage] = this.TempData[ViewDataConstants.ErrorMessage];
        }
    }
}
