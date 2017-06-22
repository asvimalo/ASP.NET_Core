using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.ViewModels;
using Gec.Services;
using Microsoft.Extensions.Configuration;


namespace Gec.Controllers.Web
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
    }
}
