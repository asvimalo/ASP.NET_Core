using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gec.Models;
using Gec.EF;

namespace Gec.Controllers.Web
{
    public class AccountController : Controller
    {
        private GecContext _ctx;

        public AccountController(GecContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Login()
        {
            return View();
        }

        
        //public IActionResult Login()
        //{
        //    return View();
        //}
    }
}
