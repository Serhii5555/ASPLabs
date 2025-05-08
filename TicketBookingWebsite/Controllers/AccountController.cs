using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.Services;
using TicketBookingWebsite.ViewModels;

namespace SportsStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender; 

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, ILogger<AccountController> logger, IEmailSender emailSender)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    await Console.Out.WriteLineAsync(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerModel);
        }

        public IActionResult Login(string returnUrl) =>
            View(new LoginModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(loginModel.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("", "Invalid name or password");
            }
            return View(loginModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var model = new UserProfileModel
            {
                Email = user.Email,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserProfileModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                user.UserName = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    ViewBag.StatusMessage = "Profile updated successfully!";
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View("Manage", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email = "")
        {
            var model = new ForgotPasswordModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model, [FromServices] IEmailSender emailSender)
        {
            if (!ModelState.IsValid) return View(model);

            InfoPageModel infoPageModel = new InfoPageModel
            {
                Message = "Password reset mail has been sent. Check your email."
            };

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("InfoPage", infoPageModel);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { token, email = model.Email }, Request.Scheme);

            await emailSender.SendEmailAsync(model.Email, "Reset Password",
                $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

            return View("InfoPage", infoPageModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult InfoPage() =>
            View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email) =>
            View(new ResetPasswordModel { Token = token, Email = email });

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid) return View(model);

            InfoPageModel infoPageModel = new InfoPageModel
            {
                Message = "Password has been reset."
            };

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("InfoPage", infoPageModel);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return View("InfoPage", infoPageModel);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
