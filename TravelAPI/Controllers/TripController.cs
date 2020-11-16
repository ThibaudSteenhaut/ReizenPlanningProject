using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository context)
        {
            _tripRepository = context;
        }

        #region Methods 
        //GET: api/Routes 
        /// <summary> 
        /// Get all routes 
        /// </summary> 
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> GetTrips()
        {
            return Ok(_tripRepository.GetAll());
        }
        #endregion
    }
}
