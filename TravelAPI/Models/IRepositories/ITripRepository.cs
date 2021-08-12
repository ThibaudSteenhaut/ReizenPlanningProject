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
        Trip GetByWithTripItems(int id);
        void Add(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);

        TripItem GetTripItem(int id);
        void AddTripItem(TripItem tripItem);
        void UpdateTripItem(TripItem tripItem);
        void DeleteTripItem(TripItem tripItem);

        IEnumerable<TripDTO> GetTrips(string userId);

        void AddTripCategory(Category category);

        void SaveChanges();
    }
}
