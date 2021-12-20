using BookMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ITokenService tokenService;
        private string generatedToken = null;
        private readonly IConfiguration _config;
        public UserController(ILogger<UserController> _logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, IConfiguration _config)
        {
            this._logger = _logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this._config = _config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.UserName, PasswordHash = model.Password };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);

                    }
                }

            }
            return View(model);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                    var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                       generatedToken = tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), model);
                       if(generatedToken!=null)
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                        throw new Exception("No JWT token generated");
                        }
                      
                    
                    }
                    else
                    {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    //return (RedirectToAction("Error"));
                }

                  
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
