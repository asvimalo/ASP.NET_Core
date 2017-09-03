 using Gec.EF.Repo;
using Gec.Models.Playground;
using Gec.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gec.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Gec.Controllers.Api
{
    [Route("api/trips")]
    
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IPlaygroundRepo _repo;

        public TripsController(IPlaygroundRepo repo, ILogger<TripsController> logger)
        {
           
            _repo = repo;
            _logger = logger;
        }
        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json(new Trip() { Name = "My Trip" });
        //}

        /// <summary>
        /// Same but we can return a bad request
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var result = _repo.GetTripsByUsername(this.User.Identity.Name);
                var allTripsVm = Mappers.ListTripsVM(_repo.GetAllTrips().ToList());
                return Ok(allTripsVm);
            }
            catch (Exception ex)
            {
                // TODO LOGGING
                _logger.LogError($"Failed to get all trips: {ex}" );
                return BadRequest("Error occured");
            }
        }
        [HttpPost("")]
        public  async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if(ModelState.IsValid)
            {
                //ClaimsPrincipal currentUser = this.User;

                var newTrip = Mappers.newTrip(trip);

                newTrip.UserName = User.Identity.Name;
                _repo.Add(newTrip);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mappers.TripVM(newTrip)); 
                }
                
            }
                
            return BadRequest("Failed to save the trip");
        }
    }
}
