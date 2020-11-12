﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public interface ITripRepository
    {
        Trip GetBy(int id);
        IEnumerable<Trip> GetAll();
        IEnumerable<Trip> GetBy(string destination= null);
        void Add(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);
        void SaveChanges();
    }
}