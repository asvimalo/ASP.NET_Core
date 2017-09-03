using Gec.EF.Repo;
using Gec.Helpers;
using Gec.Services;
using Gec.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Api
{
    [Authorize]
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<TripsController> _logger;
        private IPlaygroundRepo _repo;

        public StopsController(IPlaygroundRepo repo, 
            ILogger<TripsController> logger,
            GeoCoordsService coordsService)
        {
            _repo = repo;
            _logger = logger;
            _coordsService = coordsService;
        }
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                
                var trip = _repo.GetUserTripByName(tripName, User.Identity.Name);
                return Ok(Mappers.ListStopsVM(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {

                _logger.LogError("Failed to get stops: {0}", ex);
            }
            return BadRequest("Failed to get stops");
        }
        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //ClaimsPrincipal currentUser = this.User;

                    var newStop = Mappers.newStop(stop);

                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                    }

                    _repo.AddAStop(tripName,newStop, User.Identity.Name);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/{newStop.Name}", Mappers.StopVM(newStop));
                    }

                }
            }
            catch (Exception Ex)
            {

                _logger.LogError("Failed to save new Stop: {0}", Ex);
            }
            

            return BadRequest("Failed to save new stop");
        }
    }
}

