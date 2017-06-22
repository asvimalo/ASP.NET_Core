using Gec.EF;
using Gec.EF.Db;
using Gec.EF.IRepo;
using Gec.EF.Repo;
using Gec.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Web
{
    public class PlaygroundController : Controller
    {
        IPlaygroundRepo _repo;
        IConfigurationRoot _config;
        ILogger<PlaygroundController> _logger;

        public PlaygroundController(IPlaygroundRepo repo, IConfigurationRoot config, ILogger<PlaygroundController> logger)
        {
            _repo = repo;
            _config = config;
            _logger = logger;
        }
        public IActionResult Index()
        {            
            return View();
        }
        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                var trips = _repo.GetAllTrips();
                return View(trips);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/Error");
            }
        }
    }
}
