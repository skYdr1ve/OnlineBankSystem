using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using OnlineBankSystem.Core;

namespace OnlineBankSystem.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected IActionResult RedirectToHome() => this.RedirectToAction("Index", "Home");

        protected string GetCurrentUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        protected void ShowErrorMessage(string message)
        {
            this.TempData[GlobalConstants.TempDataErrorMessageKey] = message;
        }

        protected void ShowSuccessMessage(string message)
        {
            this.TempData[GlobalConstants.TempDataSuccessMessageKey] = message;
        }
    }
}
