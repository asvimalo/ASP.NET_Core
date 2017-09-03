using System.Collections.Generic;
using Gec.Models.Playground;
using System.Threading.Tasks;

namespace Gec.EF.Repo
{
    public interface IPlaygroundRepo
    {
        IEnumerable<Trip> GetAllTrips();
        void Add(Trip trip);
        
        void Delete(int id);
        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string username);
        
        IEnumerable<Trip> GetTripsByUsername(string name);
        void AddAStop(string tripName, Stop newStop, string username);
        Task<bool> SaveChangesAsync();
    }
}