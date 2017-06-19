using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gec.Controllers.Web
{
    public class AccountController : Controller
    {
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
