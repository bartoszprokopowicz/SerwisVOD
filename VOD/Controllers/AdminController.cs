
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VOD.Data;
using VOD.Models;
using VOD.Models.AccountViewModels;
using VOD.Services;

namespace VOD.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Uzytkownicy> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<Uzytkownicy> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pracownicy = _context.Uzytkownicy
                .Include(d => d.Daneosobowe);

            return View(await pracownicy.ToListAsync());
        }

        [HttpGet]
        public IActionResult Rejestracja()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rejestracja(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Uzytkownicy
                {
                    UserName = model.Login,
                    Email = model.Email,
                    PhoneNumber = model.NumerTelefonu,
                    Daneosobowe = new Daneosobowe
                    {
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        DataUrodzin = model.DataUrodzin
                    },
                    DataUtworzenia = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Add a user to the default role, or any role you prefer here
                    await _userManager.AddToRoleAsync(user, "Pracownik");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(System.Convert.ToString(user.Id), code, Request.Scheme);

                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}