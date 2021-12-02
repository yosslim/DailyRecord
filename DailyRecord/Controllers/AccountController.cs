using DailyRecord.Data.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyRecord.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description) ;
                }

            }

            return View(model);

        }

        //GET: /Account/Login/       
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var identityResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        //        if (identityResult.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction(nameof(HomeController.Index), "Home");
        //                //return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError("", "Username or password incorrect");
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel userModel, string returnUrl = null)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(userModel);
        //    }

        //    // Verity the credential
        //    var user = await _userManager.FindByEmailAsync(userModel.Email);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
        //    {
        //        // Creating the security context
        //        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        //        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        //        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));


        //        // Creating the security context

        //        //var claims = new List<Claim>
        //        //{
        //        //    new Claim(ClaimTypes.Name, user.UserName),
        //        //    new Claim(ClaimTypes.Email, user.Email)
        //        //};

        //        //var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        //        //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

        //        //await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

        //        return RedirectToLocal(returnUrl);

        //    }

        //    ModelState.AddModelError("", "Invalid UserName or Password");
        //    return View(userModel);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
            //return RedirectToAction("Login", "Account");
        }
    }
}
