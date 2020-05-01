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
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (identityResult.IsLockedOut)
                {
                    var donentarih = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));
                    var kalandk = donentarih.Value.Minute - DateTime.Now.Minute;
                    ModelState.AddModelError("",$"5 kere yanlış girdiğin için hesap kilitlendi {kalandk} dakika sonra giriş yapabilirsiniz");
                    return View("Index", model);
                }
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                var yanlissayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));
                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı {5-yanlissayisi} kere daha yanlış girerseniz hesap kilitlenecek");
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