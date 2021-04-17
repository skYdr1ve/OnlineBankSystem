using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Web.Areas.Identity.Models;

namespace OnlineBankSystem.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : BasePageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }

        [BindProperty, Required, Display(Name = "Code")]
        public string VerificationCode { get; set; }

        public async Task<IActionResult> OnGetAsync(string phone, string returnUrl = null)
        {
            if (phone == null)
            {
                return RedirectToHome();
            }

            var user = await _userManager.FindByNameAsync(phone);
            if (user == null)
            {
                return NotFound($"Unable to load user with phone number: '{phone}'.");
            }

            Phone = phone;
            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Phone);
            if (user == null)
            {
                return NotFound($"Unable to load user with phone number: '{Phone}'.");
            }

            var result = await _userManager.ChangePhoneNumberAsync(user, Phone, VerificationCode);
            if (result.Succeeded)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                return RedirectToHome();
            }

            ModelState.AddModelError("", "Failed to verify phone");

            return Page();
        }
    }
}
