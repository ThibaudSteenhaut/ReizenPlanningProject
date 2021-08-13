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

        #region Trip     

        IEnumerable<TripDTO> GetTrips(string userId);
        Trip GetBy(int id);
        Trip GetByWithTripItems(int id);
        void Add(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);

        #endregion

        #region TripItems

        TripItem GetTripItem(int id);
        IEnumerable<TripItem> GetTripItems(int tripId);
        void AddTripItem(TripItem tripItem);
        void UpdateTripItem(TripItem tripItem);
        void DeleteTripItem(TripItem tripItem);

        #endregion

        #region TripCategory

        IEnumerable<TripCategory> GetTripCategories(int tripId);
        TripCategory GetTripCategoryBy(int id); 
        void AddTripCategory(TripCategory category);
        void DeleteTripCategoryWithItems(TripCategory category);

        #endregion

        void SaveChanges();
    }
}
