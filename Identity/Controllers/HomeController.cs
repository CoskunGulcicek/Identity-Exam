using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contexts;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıı");
            }
            return View("Index",model);
        }
        public IActionResult SignUp()
        {
            return View(new UserSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel model)
        {
            if (ModelState.IsValid){
                AppUser user = new AppUser { 
                Email = model.Email,
                Name = model.Name,
                SurName = model.SurName,
                UserName=model.UserName
                };
                
              var result =  await _userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

    }
}