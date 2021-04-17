using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Web.Areas.Identity.Models;

namespace OnlineBankSystem.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : BasePageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult OnGet()
            => !User.Identity.IsAuthenticated ? RedirectToHome() : Page();

        public async Task<IActionResult> OnPost()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToHome();
        }
    }
}