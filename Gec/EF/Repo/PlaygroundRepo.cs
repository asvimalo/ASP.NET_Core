using System;
using System.Collections.Generic;
using System.Linq;
using Gec.EF.Db;
using Gec.Models.Playground;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace Gec.EF.Repo
{
    public class PlaygroundRepo : IPlaygroundRepo
    {
        private GecContext _ctx;
        private ILogger<PlaygroundRepo> _logger;

        public PlaygroundRepo(GecContext ctx, ILogger<PlaygroundRepo> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void Add(Trip trip)
        {
            _ctx.Trips.Add(trip);
            
        }

        public void AddAStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _ctx.Stops.Add(newStop);
            }
        }

        public void AddAStop(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _ctx.Stops.Add(newStop);
            };
        }

        public void Delete(int id)
        {
            var remove = _ctx.Trips.FirstOrDefault(x => x.Id == id);
            _ctx.Remove(remove);
            
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All trips from the Database");
            return _ctx.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            try
            {
                var trip = _ctx.Trips
                    .Include(t => t.Stops)
                    .FirstOrDefault(x => x.Name == tripName);
                return trip;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Trip> GetTripsByUsername(string name)
        {
            return _ctx.Trips
                .Include(t => t.Stops)
                .Where(t => t.UserName == name)
                .ToList();
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return _ctx.Trips
                    .Include(t => t.Stops)
                    .FirstOrDefault(x => x.Name == tripName && x.UserName == username); ;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        }
    }
}
