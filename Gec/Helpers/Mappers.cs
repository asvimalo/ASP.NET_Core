using Gec.Models.Playground;
using Gec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Helpers
{
    public static class Mappers
    {
        public static Trip newTrip(this TripViewModel trip)
        {
            var newTrip = new Trip {
                DateCreated = trip.DateCreated,
                Name = trip.Name,
               
            };
            return newTrip;
        }
        public static TripViewModel TripVM(this Trip newTrip)
        {
            var trip = new TripViewModel
            {
                DateCreated = newTrip.DateCreated,
                Name = newTrip.Name,

            };
            return trip;
        }
        public static List<Trip> ListTrips(this List<TripViewModel> trips)
        {
            var listTrips = new List<Trip>();
            foreach (var trip in trips)
            {
                listTrips.Add( new Trip
                {
                    DateCreated = trip.DateCreated,
                    Name = trip.Name,

                }); 
            }
            return listTrips;
        }
        public static List<TripViewModel> ListTripsVM(this List<Trip> trips)
        {
            var listTrips = new List<TripViewModel>();
            foreach (var trip in trips)
            {
                listTrips.Add(new TripViewModel
                {
                    DateCreated = trip.DateCreated,
                    Name = trip.Name,

                });
            }
            return listTrips;
        }
    }
}
