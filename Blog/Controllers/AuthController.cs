using System;
using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
	public class AuthController : Controller
	{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BlogDbContext _blogDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, BlogDbContext blogDbContext , UserManager<IdentityUser> userManager)
		{
            _signInManager = signInManager;
            _blogDbContext = blogDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password,false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login.Username);
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (isAdmin)
                {
                    return RedirectToAction("Index", "Panel");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM reg)
        {
            if(!ModelState.IsValid)
            {
            //_blogDbContext.
                return View(reg);
            }
            var user = new IdentityUser()
            {
                UserName = reg.Username,
                Email = reg.Email
            };

            var result = await _userManager.CreateAsync(user, reg.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
            }
    }
}

