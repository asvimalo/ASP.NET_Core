using System.Collections.Generic;
using Gec.Models.Playground;

namespace Gec.EF.Repo
{
    public interface IPlaygroundRepo
    {
        IEnumerable<Trip> GetAllTrips();
    }
}