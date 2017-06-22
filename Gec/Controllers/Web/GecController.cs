using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gec.ViewModels;
using Gec.Services;
using Microsoft.Extensions.Configuration;
using Gec.Models;
using Gec.EF;
using Gec.EF.Db;
using Gec.EF.IRepo;

namespace Gec.Controllers.Web
{
    
    public class GecController : Controller
    {
        IEmailService _emailService;
        IConfigurationRoot _config;
        IFeedRepo _feedRepo;

        public GecController(IEmailService emailService, IConfigurationRoot config, IFeedRepo feedRepo)
        {
            _emailService = emailService;
            _config = config;
            _feedRepo = feedRepo;
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
            if (model.Email.Contains("aol.com"))
                ModelState.AddModelError("Email", "We do not support AOL addresses");

            if (ModelState.IsValid)
            {
                _emailService.SendMeil(_config["MailSettings:ToAddress"], model.Email, "From Gec", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Message sent";
            }
                

            return View();
        }
    }
}
