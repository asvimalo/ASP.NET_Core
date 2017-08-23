using Gec.EF.Repo;
using Gec.Helpers;
using Gec.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Api
{
    public class StopsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IPlaygroundRepo _repo;

        public StopsController(IPlaygroundRepo repo, ILogger<TripsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repo.GetTripByName(tripName);
                return Ok(trip.Stops.OrderBy(s => s.Order).ToList());
            }
            catch (Exception ex)
            {

                _logger.LogError("Failed to get stops: {0}", ex);
            }
            return BadRequest("Failed to get stops");
        }
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                //ClaimsPrincipal currentUser = this.User;

                var newTrip = Mappers.newTrip(trip);
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

