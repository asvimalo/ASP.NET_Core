using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.EF.Db;
using Gec.Models.Playground;
using Gec.EF.IRepo;

namespace Gec.EF.Repo
{
    public class PlaygroundRepo : IPlaygroundRepo
    {
        private GecContext _ctx;
        public PlaygroundRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            return _ctx.Trips.ToList();
        }
    }
}
