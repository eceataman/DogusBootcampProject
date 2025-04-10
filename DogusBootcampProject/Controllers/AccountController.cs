using DogusBootcampProject.Models;
using DogusBootcampProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogusBootcampProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// GET: /Account/Register
		public IActionResult Register() => View();

		// POST: /Account/Register
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var user = new User { UserName = model.UserName, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in result.Errors)
				ModelState.AddModelError("", error.Description);

			return View(model);
		}

		// GET: /Account/Login
		public IActionResult Login() => View();

		// POST: /Account/Login
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

			if (result.Succeeded)
				return RedirectToAction("Index", "Home");

			ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
			return View(model);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
