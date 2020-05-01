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
        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
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