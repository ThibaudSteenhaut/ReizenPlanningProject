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
using TripTask = TravelAPI.Models.Domain.TripTask;

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


        #region Trip 

        //GET: api/Trips 
        /// <summary> 
        /// Get all future trips 
        /// </summary> 
        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<TripDTO>> GetTrips()
        {
            IdentityUser currentUser = GetCurrentUser();
            if (currentUser == null) return BadRequest();

            return Ok(_tripRepository.GetTrips(GetCurrentUser().Id));
        }

        //GET: api/Trips/Past
        /// <summary> 
        /// Get all past trips 
        /// </summary> 
        [HttpGet("Past")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<TripDTO>> GetPastTrips()
        {
            IdentityUser currentUser = GetCurrentUser();
            if (currentUser == null) return BadRequest();

            return Ok(_tripRepository.GetPastTrips(GetCurrentUser().Id));
        }



        //GET: api/Trips/{id} 
        /// <summary> 
        /// Get the trip with the certain id
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
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
        /// Delete the trip with the certain id
        /// </summary> 
        /// <param name="id">The id of the trip to delete</param> 
        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Trip> DeleteTrip(int id)
        {
            if (id < 0)
                return NotFound();

            Trip trip = _tripRepository.GetBy(id);

            if (trip == null)
                return NotFound();

            _tripRepository.Delete(trip);
            _tripRepository.SaveChanges();
            return Ok();
        }

        #endregion

        #region TripItem

        //GET: api/Trips/{id}/Tripitems
        /// <summary> 
        /// Get all tripitems of a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}/Tripitems")]
        [Authorize(Policy = "User")]
        public ActionResult<TripItemDTO> GetTripsItems(int id)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            if (id < 0) return NotFound();

            IEnumerable<TripItem> tripItems = _tripRepository.GetTripItems(id);

            if (tripItems == null)
            {
                return BadRequest();
            }

            return Ok(tripItems.ToList().Select(t => new TripItemDTO(t)));
        }

        //POST: api/Trips/{id}/TripItems
        /// <summary>
        /// Add a tripitem to the trip of the logged in user
        /// </summary>
        [HttpPost("{id}/TripItems")]
        [Authorize(Policy = "User")]
        public ActionResult<TripItemDTO> AddTripItem(int id, TripItemDTO tripItem)
        {

            if (id < 0) return NotFound();

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            TripCategory category = _tripRepository.GetTripCategoryBy(tripItem.Category.Id);
            TripItem tripItemToCreate = new TripItem(tripItem.Name, tripItem.Amount, category, tripItem.CheckedIn);

            Trip trip = _tripRepository.GetByWithTripItems(id);
            trip.TripItems.Add(tripItemToCreate);
            _tripRepository.SaveChanges();

            return Ok(tripItemToCreate.Id);
        }

        //PUT: api/Trips/TripItems
        /// <summary>
        /// Updates the trip items of the logged in user with the tripid
        /// </summary>
        [HttpPut("TripItems")]
        [Authorize(Policy = "User")]
        public ActionResult UpdateTripItems(List<TripItemDTO> tripItems)
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

        //DELETE: api/Trips/TripItems/{id}
        /// <summary> 
        /// Delete the tripitem with the certain id
        /// </summary> 
        /// <param name="id">The id of the tripitem to delete</param> 
        [HttpDelete("TripItems/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult DeleteTripItem(int id)
        {

            if (id < 0)
                return BadRequest("Not a valid trip id");

            TripItem tripitem = _tripRepository.GetTripItem(id);

            if (tripitem == null)
                return NotFound();

            _tripRepository.DeleteTripItem(tripitem);
            _tripRepository.SaveChanges();
            return Ok();
        }

        #endregion


        #region TripCategories

        //GET: api/Trip/{id}/Categories
        /// <summary> 
        /// Gets the categories of the trip of the logged in user 
        /// </summary> 
        [HttpGet("{id}/Categories")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<TripCategoryDTO>> GetTripItemCategories(int id)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            IEnumerable<TripCategoryDTO> categories = _tripRepository.GetTripCategories(id).Select(c => new TripCategoryDTO(c));

            return Ok(categories);
        }


        //POST: api/Trip/{id}/Category
        /// <summary>
        /// Add a category for items to a specific trip
        /// </summary>
        [HttpPost("{id}/Category")]
        [Authorize(Policy = "User")]
        public ActionResult<int> AddTripCategory(int id, TripCategoryDTO category)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (id < 0) return BadRequest();

            if (currentUser == null || String.IsNullOrEmpty(category.Name))
            {
                return BadRequest();
            }

            Trip trip = _tripRepository.GetBy(id);

            if(trip == null) return NotFound();

            TripCategory categoryToCreate = new TripCategory(category.Name, trip);
            _tripRepository.AddTripCategory(categoryToCreate);
            _tripRepository.SaveChanges();

            return Ok(categoryToCreate.Id);
        }


        //DELETE: api/Trip/Category/{id}
        /// <summary>
        /// Delete a tripcategory for the logged in user, all relating tripitems will be deleted as well
        /// </summary>
        [HttpDelete("Category/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult DeleteTripCategoryWithItems(int id)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            if (id < 0) return NotFound();

            TripCategory categoryToDelete = _tripRepository.GetTripCategoryBy(id);
            _tripRepository.DeleteTripCategoryWithItems(categoryToDelete);
            _itemRepository.SaveChanges();

            return Ok();

        }


        #endregion

        #region Activities 

        //GET: api/Trips/{id}/TripTasks
        /// <summary> 
        /// Get all tasks from the trip
        /// </summary> 
        [HttpGet("{id}/TripTasks")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<TripTask>> GetTripTasks(int id)
        {
            IdentityUser currentUser = GetCurrentUser();
            if (currentUser == null) return BadRequest();

            return Ok(_tripRepository.GetTripTasks(id));
        }

        //POST: api/Trip/{id}/TripTask
        /// <summary> 
        /// Add a new task to the trip
        /// </summary> 
        [HttpPost("{id}/TripTask")]
        [Authorize(Policy = "User")]
        public ActionResult<int> PostTripTask(int id, TripTask activity)
        {

            if (id < 0) return NotFound();

            IdentityUser currentUser = GetCurrentUser();
            if (currentUser == null) return BadRequest();
           
            Trip trip = _tripRepository.GetByWithActivities(id);

            if (trip == null) return NotFound();

            trip.TripTasks.Add(activity);
            _tripRepository.SaveChanges();
            return Ok(activity.Id);
        }


        //DELETE: api/Trips/TripTask/{tripTaskId}
        /// <summary> 
        /// Delete the task from the trip
        /// </summary> 
        [HttpDelete("TripTask/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult DeleteTripTask(int id)
        {

            if (id < 0) return NotFound();

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            
            TripTask a = _tripRepository.GetTripTaskBy(id);
            _tripRepository.DeleteTripTask(a);
            _tripRepository.SaveChanges();
            return Ok();
        }

        //PUT: api/Trips/TripTasks
        /// <summary>
        /// Updates the trip tasks of the logged in user
        /// </summary>
        [HttpPut("TripTasks")]
        [Authorize(Policy = "User")]
        public ActionResult UpdateTripTasks(IEnumerable<TripTask> tripTasks)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            _tripRepository.UpdateTripTasks(tripTasks);
            _tripRepository.SaveChanges(); 

            return Ok();
        }



        #endregion

        #region ItineraryItems 

        //GET: api/Trips/{id}/ItineraryItems
        /// <summary> 
        /// Get all Itinerary items of a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}/ItineraryItems")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<ItineraryItemDTO>> GetItineraryItems(int id)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            if (id < 0) return NotFound();

            IEnumerable<ItineraryItem> itineraryItems = _tripRepository.GetItineraryItems(id);

            if (itineraryItems == null)
            {
                return BadRequest();
            }

            return Ok(itineraryItems.ToList().Select(ii => new ItineraryItemDTO(ii)));
        }

        //POST: api/Trips/{id}/ItineraryItem
        /// <summary>
        /// Add a itineraryitem to the trip of the logged in user
        /// </summary>
        [HttpPost("{id}/ItineraryItem")]
        [Authorize(Policy = "User")]
        public ActionResult<TripItemDTO> AddItineraryItem(int id, ItineraryItem itineraryItem)
        {

            if (id < 0) return NotFound();

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            ItineraryItem iiToCreate = new ItineraryItem(itineraryItem.Description, itineraryItem.Date);
            Trip trip = _tripRepository.GetByWithItineraryItems(id);
            trip.ItineraryItems.Add(iiToCreate);
            _tripRepository.SaveChanges();

            return Ok(iiToCreate.Id);
        }


        //DELETE: api/Trips/ItineraryItem/{id}
        /// <summary> 
        /// Delete the tripitem with the certain id
        /// </summary> 
        /// <param name="id">The id of the tripitem to delete</param> 
        [HttpDelete("ItineraryItem/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult DeleteItineraryItem(int id)
        {

            if (id < 0) return BadRequest();

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            ItineraryItem itineraryItem = _tripRepository.GetItineraryItemBy(id);

            if (itineraryItem == null)
                return NotFound();

            _tripRepository.DeleteItineraryItem(itineraryItem);
            _tripRepository.SaveChanges();
            return Ok();
        }

        #endregion


        private IdentityUser GetCurrentUser()
        {
            return _userManager.FindByNameAsync(User.Identity.Name).Result;
        }
        #endregion     
    }
}
