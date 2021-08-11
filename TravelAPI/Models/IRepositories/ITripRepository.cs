using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public interface ITripRepository
    {
        Trip GetBy(int id);
        IEnumerable<TripDTO> GetTrips(string userId);
        TripItem GetTripItem(int id);
        void AddTripItem(TripItem tripItem);
        void UpdateTripItem(TripItem tripItem);
        void AddTripCategory(Category category);
        void Add(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);
        void SaveChanges();
    }
}
