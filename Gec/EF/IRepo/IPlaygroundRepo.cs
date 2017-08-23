using System.Collections.Generic;
using Gec.Models.Playground;
using System.Threading.Tasks;

namespace Gec.EF.Repo
{
    public interface IPlaygroundRepo
    {
        IEnumerable<Trip> GetAllTrips();
        void Add(Trip trip);
        Task<bool> SaveChangesAsync();
        void Delete(int id);
        Trip GetTripByName(string tripName);
    }
}