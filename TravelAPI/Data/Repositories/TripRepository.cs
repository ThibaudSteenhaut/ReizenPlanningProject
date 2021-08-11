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
        private readonly DbSet<Category> _categories;
        #endregion

        public TripRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.Trips;
            _tripItems = dbContext.TripItems;
            _categories = dbContext.Categories;
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Delete(Trip trip)
        {
            _trips.Remove(trip);
        }

        public TripItem GetTripItem(int id)
        {
            return _tripItems.SingleOrDefault(ti => ti.Id == id);
        }

        public void UpdateTripItem(TripItem tripItem)
        {
            _tripItems.Update(tripItem);
        }

        public IEnumerable<TripDTO> GetTrips(string userId)
        {
            return _trips.Where(t => t.User.Id == userId).Where(t => t.ReturnDate > DateTime.Now).OrderBy(t => t.DepartureDate).Select(t => new TripDTO(t)); 
        }

        public void AddTripCategory(Category category)
        {
            _categories.Add(category);
        }

        public Trip GetBy(int id)
        {
            return _trips.SingleOrDefault(r => r.Id == id);
        }

        public void AddTripItem(TripItem tripItem)
        {
            _context.TripItems.Add(tripItem);
        }

        public void Update(Trip trip)
        {
            _trips.Update(trip);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
