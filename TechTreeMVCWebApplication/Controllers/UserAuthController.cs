namespace TechTreeMVCWebApplication.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TechTreeMVCWebApplication.Data;
    using TechTreeMVCWebApplication.Models;

    public class UserAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserAuthController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.LoginInValid = "true";

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    model.LoginInValid = string.Empty;
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return PartialView("_UserLoginPartial", model);
        }

        public async Task<IActionResult> Logоut(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
