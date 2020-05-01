using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class FamaleController : Controller
    {

        UserManager<AppUser> _userManager;
        public FamaleController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "FemalePolicy")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddClaim(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if ((await _userManager.GetClaimsAsync(user)).Count == 0)

            {
                Claim claim = new Claim("gender", "female");
                await _userManager.AddClaimAsync(user, claim);
                
            }
            return RedirectToAction("UserList", "Rol");
        }
    }
}