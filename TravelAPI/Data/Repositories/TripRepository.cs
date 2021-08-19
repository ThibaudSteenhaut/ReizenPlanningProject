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
        private readonly DbSet<TripTask> _tripTasks;
        private readonly DbSet<ItineraryItem> _itineraryItems;

        #endregion

        #region Constructor 

        public TripRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.Trips;
            _tripItems = dbContext.TripItems;
            _categories = dbContext.TripCategories;
            _tripTasks = dbContext.TripTasks;
            _itineraryItems = dbContext.ItineraryItems; 
        }

        #endregion

        #region Trip 

        public IEnumerable<TripDTO> GetTrips(string userId)
        {
            return _trips.Include(t => t.TripItems)
                .Include(t => t.TripTasks)
                .Where(t => t.User.Id == userId)
                .Where(t => t.ReturnDate > DateTime.Now)
                .OrderBy(t => t.DepartureDate)
                .Select(t => new TripDTO(t));
        }

        public IEnumerable<TripDTO> GetPastTrips(string userId)
        {
            return _trips.Include(t => t.TripItems)
                .Include(t => t.TripTasks)
                .Where(t => t.User.Id == userId)
                .Where(t => t.ReturnDate < DateTime.Now)
                .OrderBy(t => t.DepartureDate)
                .Select(t => new TripDTO(t));
        }

        public Trip GetBy(int id)
        {
            return _trips
                .Include(t => t.TripItems)
                .Include(t => t.TripTasks)
                .Include(t => t.ItineraryItems)
                .SingleOrDefault(t => t.Id == id);
        }

        public Trip GetByWithTripItems(int id)
        {
            return _trips.Include(t => t.TripItems).SingleOrDefault(t => t.Id == id);
        }

        public Trip GetByWithActivities(int id)
        {
            return _trips.Include(t => t.TripTasks).SingleOrDefault(t => t.Id == id);
        }

        public Trip GetByWithItineraryItems(int id)
        {
            return _trips.Include(t => t.ItineraryItems).SingleOrDefault(t => t.Id == id);
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Delete(Trip trip)
        {
            _tripItems.RemoveRange(trip.TripItems);
            _tripTasks.RemoveRange(trip.TripTasks);
            _itineraryItems.RemoveRange(trip.ItineraryItems);
            _categories.RemoveRange(_categories.Include(c => c.Trip).Where(c => c.Trip.Id == trip.Id));
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

        #region TripTasks 

        public TripTask GetTripTaskBy(int id)
        {
            return _tripTasks.SingleOrDefault(a => a.Id == id); 
        }

        public IEnumerable<TripTask> GetTripTasks(int tripId)
        {
            return _trips.Include(t => t.TripTasks).SingleOrDefault(t => t.Id == tripId).TripTasks.OrderBy(a => a.Description);
        }

        public void DeleteTripTask(TripTask tripTask)
        {
            _tripTasks.Remove(tripTask);
        }

        public void UpdateTripTasks(IEnumerable<TripTask> tripTasks)
        {
            _tripTasks.UpdateRange(tripTasks);
        }

        #endregion

        #region ItineraryItems

        public ItineraryItem GetItineraryItemBy(int id)
        {
            return _itineraryItems.SingleOrDefault(ii => ii.Id == id);
        }

        public IEnumerable<ItineraryItem> GetItineraryItems(int tripId)
        {
            return _trips.Include(t => t.ItineraryItems).SingleOrDefault(t => t.Id == tripId).ItineraryItems.OrderBy(ii => ii.Date);
        }

        public void DeleteItineraryItem(ItineraryItem itineraryItem)
        {
            _itineraryItems.Remove(itineraryItem);
        }

        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
