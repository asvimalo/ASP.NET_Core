using Gec.EF;
using Gec.EF.Db;
using Gec.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Web
{
    public class PlaygroundController : Controller
    {
        private GecContext _ctx;

        public PlaygroundController(GecContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            var trips = _ctx.Trips.ToList();
            return View(trips);
        }
    }
}
