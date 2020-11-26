using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        public TripController(ITripRepository tripRepo, ICategoryRepository categoryRepo, IItemRepository itemRepo)
        {
            _tripRepository = tripRepo;
            _categoryRepository = categoryRepo;
            _itemRepository = itemRepo;
        }

        #region Methods

        //GET: api/Trips 
        /// <summary> 
        /// Get all routes 
        /// </summary> 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Trip>> GetTrips()
        {
            Debug.WriteLine("get all tripssssssssssssssssssssssssssssssss");
            return Ok(_tripRepository.GetAll());
        }

        //GET: api/Trips/{id} 
        /// <summary> 
        /// Get a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult<Trip> PostTrip([FromBody] TripDTO tripDTO)
        {

            if (!ModelState.IsValid) 
            {
                return BadRequest("Invalid model");
            } 

            if (tripDTO == null)
            {
                return NotFound();
            }

            Trip tripToCreate = new Trip(tripDTO);
            _tripRepository.Add(tripToCreate);
            _tripRepository.SaveChanges();
            return CreatedAtAction(nameof(GetTrip), new { id = tripToCreate.Id }, tripToCreate);

        }

        //DELETE: api/Trip/{id} 
        /// <summary> 
        /// Delete a trip 
        /// </summary> 
        /// <param name="id">The id of the trip to delete</param> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult<TripItem> AddItemToTrip(int tripId, int itemId, int amount)
        {

            if (tripId < 0) return BadRequest();
            if (itemId < 0) return BadRequest();

            Item item = _itemRepository.GetBy(itemId);
            Trip trip = _tripRepository.GetBy(tripId);

            if(item == null || trip == null)
            {
                return NotFound(); 
            }

            TripItem tripItem = new TripItem() { Trip = trip, Item = item, Amount = amount };

            _tripRepository.AddTripItem(tripItem);
            _tripRepository.SaveChanges();
            return Ok();

        }


        #endregion     
    }
}
