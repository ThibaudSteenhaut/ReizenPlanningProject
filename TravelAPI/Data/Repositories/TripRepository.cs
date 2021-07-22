using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Trip> _trips;
        #endregion

        public TripRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.Trips;
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Delete(Trip trip)
        {
            _trips.Remove(trip);
        }

        public IEnumerable<TripDTO> GetTrips(string userId)
        {
            return _trips.Where(t => t.User.Id == userId).OrderBy(t => t.DepartureDate).Select(t => new TripDTO(t)); 
        }

        public Trip GetBy(int id)
        {
            return _trips.SingleOrDefault(r => r.Id == id);
        }

        public void AddTripItem(TripItem tripItem)
        {
            _context.TripItems.Add(tripItem);
        }

        public IEnumerable<Trip> GetBy(string destination = null)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Trip trip)
        {
            _trips.Update(trip);
        }

        public IEnumerable<TripItem> GetItemsBy(int id)
        {
            return _trips.Include(t => t.TripItems).ThenInclude(ti => ti.Item).SingleOrDefault(r => r.Id == id).TripItems;
        }
    }
}
