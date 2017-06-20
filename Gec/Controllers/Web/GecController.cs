using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gec.ViewModels;
using Gec.Services;

namespace Gec.Controllers.Web
{
    
    public class GecController : Controller
    {
        ImailService _emailService;
        public GecController(ImailService emailService)
        {
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Historia()
        {
            return View();
        }
        public IActionResult Contact()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            _emailService.SendMeil("asvimalo@gmail.com", model.Email, "From Gec", model.Message);
            return View();
        }
    }
}
