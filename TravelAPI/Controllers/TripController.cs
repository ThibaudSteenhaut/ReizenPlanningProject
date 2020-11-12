using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository context)
        {
            _tripRepository = context;
        }

        //GET: api/Trips
        [HttpGet]
        public IEnumerable<Trip> GetTrips(string destination = null)
        {
            if (string.IsNullOrEmpty(destination))
                return _tripRepository.GetAll();
            return _tripRepository.GetBy(destination);

        }
    }
}
