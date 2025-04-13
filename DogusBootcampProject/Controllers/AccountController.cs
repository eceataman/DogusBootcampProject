using DogusBootcampProject.Models;
using DogusBootcampProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogusBootcampProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
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
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Bu e-posta ile kayıtlı kullanıcı bulunamadı.";
                return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = model.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama", $"<a href='{link}'>Şifrenizi sıfırlamak için tıklayın</a>");

            TempData["Success"] = "Şifre sıfırlama bağlantısı mail adresinize gönderildi.";
            return RedirectToAction("ForgotPassword");
        }


        public IActionResult ForgotPasswordConfirmation() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email) =>
    View(new ResetPasswordViewModel { Token = token, Email = email });

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        public IActionResult ResetPasswordConfirmation() => View();

    }
}
