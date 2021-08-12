using ReizenPlanningProject.Model.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Repositories
{
    public interface ITripRepository
    {

        ObservableCollection<Trip> GetTrips();
        ObservableCollection<TripItem> GetTripItems(int tripId);
        void UpdateTripItems(int tripId, List<TripItem> tripItems);
        List<Category> GetTripCategories(int tripId);
        Task<int> AddTripItem(int tripId, TripItem tripItem);
        void DeleteTripItem(int tripItemId);
        Task<int> AddTripCategory(int tripId, Category category);
        Task<int> Add(Trip trip);
        Task<bool> Remove(int tripId);
    }
}
