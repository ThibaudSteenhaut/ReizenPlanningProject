using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TripController(ITripRepository tripRepo, IItemRepository itemRepo, UserManager<IdentityUser> userManager)
        {
            _tripRepository = tripRepo;
            _itemRepository = itemRepo;
            _userManager = userManager;
        }

        #region Methods

        //GET: api/Trips 
        /// <summary> 
        /// Get all routes 
        /// </summary> 
        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<TripDTO>> GetTrips()
        {
            return Ok(_tripRepository.GetTrips(GetCurrentUser().Id));
        }

        //GET: api/Trips/{id} 
        /// <summary> 
        /// Get a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TripDTO> GetTrip(int id)
        {
            Trip trip = _tripRepository.GetBy(id);
            if (trip == null)
                return NotFound();
            return Ok(new TripDTO(trip));
        }

        //POST: api/Trip 
        /// <summary> 
        /// Add a new trip
        /// </summary> 
        /// <param name="tripDTO">The trip to add</param> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "User")]
        public ActionResult<int> PostTrip([FromBody] TripDTO tripDTO)
        {

            if (!ModelState.IsValid || tripDTO == null) 
                return BadRequest("Invalid model");
                      
            Trip tripToCreate = new Trip(tripDTO, GetCurrentUser());
            _tripRepository.Add(tripToCreate);
            _tripRepository.SaveChanges();
            return Ok(tripToCreate.Id);

        }

        //DELETE: api/Trip/{id} 
        /// <summary> 
        /// Delete a trip 
        /// </summary> 
        /// <param name="id">The id of the trip to delete</param> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "User")]
        public ActionResult<Trip> DeleteTrip(int id)
        {

            if (id < 0)
                return BadRequest("Not a valid trip id");

            Trip trip = _tripRepository.GetBy(id);

            if (trip == null)
                return NotFound();

            _tripRepository.Delete(trip);
            _tripRepository.SaveChanges();
            return Ok();
        }

        //POST: api/Trip/{id}/Item/{itemId}
        /// <summary> 
        /// Add an existing item to a trip with a certain amount 
        /// for example : add 5 t-shirts to your trip to canada (t-shirts is already in the category 'clothing')
        /// </summary> 
        /// <param name="tripId">The id of the trip</param> 
        /// <param name="categoryId">The id of the category</param> 
        /// <param name="itemId">The id of the item</param> 
        /// <param name="amount">How many times you want to take this item</param> 
        [HttpPost("{tripId}/Item/{itemId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "User")]
        public ActionResult<TripItem> AddItemToTrip(int tripId, int itemId, int amount)
        {

            //if (tripId < 0) return BadRequest();
            //if (itemId < 0) return BadRequest();

            //Item item = _itemRepository.GetBy(itemId);
            //Trip trip = _tripRepository.GetBy(tripId);

            //if(item == null || trip == null)
            //{
            //    return NotFound(); 
            //}

            //TripItem tripItem = new TripItem() { Trip = trip, Item = item, Amount = amount };

            //_tripRepository.AddTripItem(tripItem);
            //_tripRepository.SaveChanges();
            return Ok();

        }

        private IdentityUser GetCurrentUser()
        {
            return _userManager.FindByNameAsync(User.Identity.Name).Result; 
        }
        #endregion     
    }
}
