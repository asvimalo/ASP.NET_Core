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
        public static Stop newStop(this StopViewModel stop)
        {
            var newStop = new Stop
            {
                Order = stop.Order,
                Name = stop.Name,
                Longitude = stop.Longitude,
                Latitude = stop.Latitude,
                Arrival = stop.Arrival

            };
            return newStop;
        }
        public static StopViewModel StopVM(this Stop newStop)
        {
            var stop = new StopViewModel
            {
                Order = newStop.Order,
                Name = newStop.Name,
                Longitude = newStop.Longitude,
                Latitude = newStop.Latitude,
                Arrival = newStop.Arrival

            };
            return stop;
        }
        public static List<Stop> ListStops(this List<StopViewModel> stops)
        {
            var listStops = new List<Stop>();
            foreach (var stop in stops)
            {
                listStops.Add(new Stop
                {
                    Order = stop.Order,
                    Longitude = stop.Longitude,
                    Latitude = stop.Latitude,
                    Name = stop.Name,
                    Arrival = stop.Arrival

                });
            }
            return listStops;
        }
        public static List<StopViewModel> ListStopsVM(this List<Stop> stops)
        {
            var listStops = new List<StopViewModel>();
            foreach (var stop in stops)
            {
                listStops.Add(new StopViewModel
                {
                    Order = stop.Order,
                    Longitude = stop.Longitude,
                    Latitude = stop.Latitude,
                    Name = stop.Name,
                    Arrival = stop.Arrival

                });
            }
            return listStops;
        }
    }
}
