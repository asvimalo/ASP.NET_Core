using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.EF.Db;
using Gec.Models.Playground;
using Gec.EF.IRepo;
using Microsoft.Extensions.Logging;

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

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All trips from the Database");
            return _ctx.Trips.ToList();
        }
    }
}
