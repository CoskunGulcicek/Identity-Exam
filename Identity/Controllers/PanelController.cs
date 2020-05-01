using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contexts;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PanelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel updateModel = new UserUpdateViewModel
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                SurName = user.SurName,
                PictureUrl = user.PictureUrl
            };

            return View(updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (model.Picture != null)
                {
                    var uygulamaninCalistigiYer = Directory.GetCurrentDirectory();
                    var uzanti = Path.GetExtension(model.Picture.FileName);
                    var resimAdi = Guid.NewGuid() + uzanti;
                    var kaydedilecekYer = uygulamaninCalistigiYer + "/wwwroot/img/" + resimAdi;

                    using var stream = new FileStream(kaydedilecekYer, FileMode.Create);
                    await model.Picture.CopyToAsync(stream);
                    user.PictureUrl = resimAdi;
                }
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
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

        [AllowAnonymous]
        public IActionResult IndexAll()
        {

            return View();
        }
    }
}