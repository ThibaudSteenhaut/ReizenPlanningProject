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
        IEnumerable<TripDTO> GetPastTrips(string userId); 
        Trip GetBy(int id);
        Trip GetByWithTripItems(int id);
        Trip GetByWithActivities(int id);
        Trip GetByWithItineraryItems(int id);
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

        #region TripTask

        TripTask GetTripTaskBy(int id);
        IEnumerable<TripTask> GetTripTasks(int tripId);
        void UpdateTripTasks(IEnumerable<TripTask> tripTasks);
        void DeleteTripTask(TripTask tripTask);

        #endregion

        #region Itinerary

        ItineraryItem GetItineraryItemBy(int id); 
        IEnumerable<ItineraryItem> GetItineraryItems(int tripId); 
        void DeleteItineraryItem(ItineraryItem itineraryItem);

        #endregion

        void SaveChanges();
    }
}
