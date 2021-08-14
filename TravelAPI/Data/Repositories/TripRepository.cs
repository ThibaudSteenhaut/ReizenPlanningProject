using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Trip> _trips;
        private readonly DbSet<TripItem> _tripItems;
        private readonly DbSet<TripCategory> _categories;
        private readonly DbSet<Activity> _activities;

        #endregion

        #region Constructor 

        public TripRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.Trips;
            _tripItems = dbContext.TripItems;
            _categories = dbContext.TripCategories;
            _activities = dbContext.Activities;
        }

        #endregion

        #region Trip 

        public IEnumerable<TripDTO> GetTrips(string userId)
        {
            return _trips.Where(t => t.User.Id == userId).Where(t => t.ReturnDate > DateTime.Now).OrderBy(t => t.DepartureDate).Select(t => new TripDTO(t));
        }

        public Trip GetBy(int id)
        {
            return _trips.SingleOrDefault(t => t.Id == id);
        }

        public Trip GetByWithTripItems(int id)
        {
            return _trips.Include(t => t.TripItems).SingleOrDefault(t => t.Id == id);
        }

        public Trip GetByWithActivities(int id)
        {
            return _trips.Include(t => t.Activities).SingleOrDefault(t => t.Id == id);
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Delete(Trip trip)
        {
            _trips.Remove(trip);
        }

        public void Update(Trip trip)
        {
            _trips.Update(trip);
        }

        #endregion

        #region TripItem

        public IEnumerable<TripItem> GetTripItems(int tripId)
        {
            return _trips.Include(t => t.TripItems).ThenInclude(ti => ti.Category).SingleOrDefault(t => t.Id == tripId).TripItems;
        }

        public TripItem GetTripItem(int id)
        {
            return _tripItems.SingleOrDefault(ti => ti.Id == id);
        }

        public void AddTripItem(TripItem tripItem)
        {
            _tripItems.Add(tripItem);
        }

        public void DeleteTripItem(TripItem tripItem)
        {
            _tripItems.Remove(tripItem);
        }

        public void UpdateTripItem(TripItem tripItem)
        {
            _tripItems.Update(tripItem);
        }

        #endregion

        #region TripCategory


        public IEnumerable<TripCategory> GetTripCategories(int tripId)
        {
            return _categories.Where(c => c.Trip.Id == tripId);
        }

        public TripCategory GetTripCategoryBy(int id)
        {
            return _categories.SingleOrDefault(c => c.Id == id); 
        }

        public void AddTripCategory(TripCategory category)
        {
            _categories.Add(category);
        }

        public void DeleteTripCategoryWithItems(TripCategory category)
        {
            _tripItems.RemoveRange(_tripItems.Include(i => i.Category).Where(i => i.Category.Id == category.Id));
            _categories.Remove(category);
        }

        #endregion

        #region Activities 

        public Activity GetActivityBy(int id)
        {
            return _activities.SingleOrDefault(a => a.Id == id); 
        }

        public IEnumerable<Activity> GetActivities(int tripId)
        {
            return _trips.Include(t => t.Activities).SingleOrDefault(t => t.Id == tripId).Activities.OrderBy(a => a.Day);
        }

        public void DeleteActivity(Activity activity)
        {
            _activities.Remove(activity);
        }

        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
