﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _context;
        private readonly DbSet<Trip> _trips;
        
        public TripRepository(TripContext dbContext)
        {
            _context = dbContext;
            _trips = dbContext.trips;
        }

        public void Add(Trip trip)
        {
            _trips.Add(trip);
        }

        public void Delete(Trip trip)
        {
            _trips.Remove(trip);
        }

        public IEnumerable<Trip> GetAll()
        {
            return _trips.ToList();
        }

        public Trip GetBy(int id)
        {
            return _trips.SingleOrDefault(r => r.Id == id);
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
    }
}