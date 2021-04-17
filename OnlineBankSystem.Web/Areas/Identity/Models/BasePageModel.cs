using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBankSystem.Core;

namespace OnlineBankSystem.Web.Areas.Identity.Models
{
    public abstract class BasePageModel : PageModel
    {
        public IActionResult RedirectToHome() => this.RedirectToAction("Index", "Home");

        public IActionResult RedirectToLoginPage()
            => this.RedirectToPage("/Account/Login");

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
