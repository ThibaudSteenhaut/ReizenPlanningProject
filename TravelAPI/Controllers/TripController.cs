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
using TravelAPI.Models.Domain;

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

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            Trip tripToCreate = new Trip(tripDTO, currentUser);
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

        //GET: api/Trips/{id}/tripitems
        /// <summary> 
        /// Get all trip items of a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}/Tripitems")]
        [Authorize(Policy = "User")]
        public ActionResult<TripItemDTO> GetTripsItems(int id)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            if (id < 0) return NotFound();

            IEnumerable<TripItem> tripItems = _itemRepository.GetTripItems(id);

            if (tripItems == null)
            {
                return BadRequest();
            }

            return Ok(tripItems.ToList().Select(t => new TripItemDTO(t)));
        }

        //PUT: api/Trips/{id}/Trip
        /// <summary>
        /// Updates the trip items of the logged in user with the tripid
        /// </summary>
        [HttpPut("{id}/TripItems")]
        [Authorize(Policy = "User")]
        public ActionResult UpdateItems(List<TripItemDTO> tripItems)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            foreach (TripItemDTO tripItem in tripItems)
            {

                TripItem ti = _tripRepository.GetTripItem(tripItem.Id);
                ti.CheckedIn = tripItem.CheckedIn;
                _tripRepository.UpdateTripItem(ti);
                _tripRepository.SaveChanges();
            }

            return Ok();
        }

        //GET: api/Trip/{id}/Categories
        /// <summary> 
        /// Gets the categories of the logged in user
        /// </summary> 
        [HttpGet("{id}/Categories")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<CategoryDTO>> GetTripItemCategories(int id)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            IEnumerable<CategoryDTO> categories = _itemRepository.GetTripCategories(GetCurrentUser().Id, id).Select(c => new CategoryDTO(c));

            return Ok(categories);
        }


        //POST: api/Trip/{id}/Category
        /// <summary>
        /// Add a category for items to a specific trip
        /// </summary>
        [HttpPost("{id}/Category")]
        [Authorize(Policy = "User")]
        public ActionResult<int> AddTripCategory(int id, CategoryDTO category)
        {

            Debug.WriteLine(id);
            Debug.WriteLine(category); 

            IdentityUser currentUser = GetCurrentUser();

            if (id < 0) return BadRequest();

            if (currentUser == null || String.IsNullOrEmpty(category.Name))
            {
                return BadRequest();
            }

            Trip trip = _tripRepository.GetBy(id);

            if(trip == null) return NotFound();
            
            Category categoryToCreate = new Category(category.Name, currentUser, false, trip);
            _itemRepository.AddCategory(categoryToCreate);
            _itemRepository.SaveChanges();

            return Ok(categoryToCreate.Id);

        }

        private IdentityUser GetCurrentUser()
        {
            return _userManager.FindByNameAsync(User.Identity.Name).Result;
        }
        #endregion     
    }
}
