using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication9.Models;
using WebApplication9.ViewModels;

namespace WebApplication9.Controllers;

public class AccountController : Controller
{
    UserManager<AppUser> _userManager;
    SignInManager<AppUser> _signInManager;
    RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("error", "yanlis");
            return View(registerVM);
        }
        AppUser appUser = new AppUser()
        {
            Name = registerVM.Name,
            Email = registerVM.Email,
            UserName = registerVM.Name,
            Surname = registerVM.Surname,
        };

        var result = await _userManager.CreateAsync(appUser, registerVM.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(appUser, "Admin");
            return RedirectToAction("Login");
        }


        return View();
    }


    public IActionResult LogOut()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("error", "model error");
        }

        AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

        if (appUser == null)
        {
            ModelState.AddModelError("user", "user tapilmadi");
            return View(loginVM);
        }

        var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.RememberMe, false);

        if (result.Succeeded)
        {
            if (returnUrl != null)
            {
                return RedirectPermanent(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        return View();
    }

    public async Task<IActionResult> CreateRoles()
    {
        var role1 = new IdentityRole("Admin");
        var role2 = new IdentityRole("Member");

        await _roleManager.CreateAsync(role1);
        await _roleManager.CreateAsync(role2);

        return RedirectToAction("Register");
    }
}
