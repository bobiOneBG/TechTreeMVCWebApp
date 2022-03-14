namespace TechTreeMVCWebApplication.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TechTreeMVCWebApplication.Data;
    using TechTreeMVCWebApplication.Entities;
    using TechTreeMVCWebApplication.Models;

    public class UserAuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserAuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
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

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> RegisterUser(RegistrationModel model)
        {
            model.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    model.RegistrationInValid = "";

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (model.CategoryId != 0)
                    {
                        await AddCategoryToUser(user.Id, model.CategoryId);
                    }

                    return PartialView("_UserRegistrationPartial", model);
                }

                else
                {
                    AddErrorsToModelState(result);
                }
            }

            return PartialView("_UserRegistrationPartial", model);
        }

        private async Task AddCategoryToUser(string userId, int categoryId)
        {
            var userCategory = new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId,
            };

            _context.UserCategory.Add(userCategory);

            await _context.SaveChangesAsync();
        }

        [AllowAnonymous]
        public async Task<bool> UserNameExist(string userName)
        {
            bool userNameExist = await _context.Users
                .AnyAsync(user => user.UserName.ToUpper() == userName.ToUpper());

            return userNameExist;
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
