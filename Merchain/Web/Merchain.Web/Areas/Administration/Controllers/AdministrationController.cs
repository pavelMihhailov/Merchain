namespace Merchain.Web.Areas.Administration.Controllers
{
    using Merchain.Common;
    using Merchain.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(AreaConstants.Administration)]
    public class AdministrationController : BaseController
    {
    }
}
