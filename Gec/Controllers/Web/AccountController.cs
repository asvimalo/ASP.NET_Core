using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gec.Models;
using Gec.EF;
using Gec.EF.Db;
using Microsoft.AspNetCore.Identity;
using Gec.Models.Account;
using Gec.ViewModels;

namespace Gec.Controllers.Web
{
    public class AccountController : Controller
    {
        private GecContext _ctx;
        private SignInManager<User> _signInManager;

        public AccountController(GecContext ctx, SignInManager<User> signInManager)
        {
            _ctx = ctx;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Trips", "Playground");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                        vm.Password, 
                                        true, // persistent
                                        false);//lockout disabled

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Trips", "Playgroud");
                    }
                    else
                        Redirect(returnUrl);
                }
                else
                    ModelState.AddModelError("", "Username or password incorrect");
            }
            return View();
           
        }


        //public IActionResult Login()
        //{
        //    return View();
        //}
    }
}
