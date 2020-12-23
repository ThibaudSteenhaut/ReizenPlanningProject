using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ReizenPlanningProject.Data
{
    class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Trip> _trips;

        public TripRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.Trips;
        }      

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _trips.ToListAsync();
        }

        public Task<Trip> GetTripByDestinationAsync(string destination)
        {
            return _trips.FirstOrDefaultAsync(t => t.Destination == destination);
        }
        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
