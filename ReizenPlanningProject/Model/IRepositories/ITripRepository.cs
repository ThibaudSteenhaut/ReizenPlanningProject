﻿using ReizenPlanningProject.Model.Domain;
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

        #region Trip

        ObservableCollection<Trip> GetTrips();
        Task<int> Add(Trip trip);
        Task<bool> Remove(int tripId);

        #endregion

        #region TripItem

        ObservableCollection<TripItem> GetTripItems(int tripId);
        void UpdateTripItems(List<TripItem> tripItems);
        Task<int> AddTripItem(int tripId, TripItem tripItem);
        void DeleteTripItem(int tripItemId);

        #endregion

        #region TripCategory

        List<Category> GetTripCategories(int tripId);
        Task<int> AddTripCategory(int tripId, Category category);
        void DeleteCategoryWithItems(int categoryId);

        #endregion

        #region Activity 

        List<Activity> GetActivities(int tripId);
        Task<int> AddActivity(int tripId, Activity activity);
        void DeleteActivity(int activityId); 

        #endregion
    }
}
