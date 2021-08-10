using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;

namespace TravelAPI.Models
{
    public interface ITripRepository
    {
        Trip GetBy(int id);
        IEnumerable<TripDTO> GetTrips(string userId);
        void AddTripItem(TripItem tripItem);
        void Add(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);
        void SaveChanges();
    }
}
