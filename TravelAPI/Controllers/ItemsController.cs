using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelAPI.DTOs;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly ITripRepository _tripRepository;
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ItemsController(ITripRepository tripRepo, IItemRepository itemRepo, UserManager<IdentityUser> userManager)
        {

            _tripRepository = tripRepo;
            _itemRepository = itemRepo;
            _userManager = userManager;
        }

        #region Methods 

        //GET: api/Items
        /// <summary> 
        /// Gets the items of the logged in user
        /// </summary> 
        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<ItemDTO>> GetItems()
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            IEnumerable<ItemDTO> items = _itemRepository.GetItems(GetCurrentUser().Id).Select(i => new ItemDTO(i));

            return Ok(items);
        }

        //GET: api/Trips/{id}/Items
        /// <summary> 
        /// Get all items of a trip 
        /// </summary> 
        /// <param name="id">The id of the trip</param> 
        [HttpGet("{id}/items")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "User")]
        public ActionResult<TripDTO> GetItemsBy(int id)
        {

            //if (id < 0)
            //    return NotFound("Id can't be found");

            //IEnumerable<TripItem> tripItems = _tripRepository.GetItemsBy(id);

            //if (tripItems == null)
            //    return NotFound("Id can't be found");

            //IEnumerable<ItemDTO> itemDTOs = tripItems.Select(ti => new ItemDTO(ti.Item.Name, ti.Item.Category, ti.Amount));

            //return Ok(itemDTOs);
            return Ok();
        }

        private IdentityUser GetCurrentUser()
        {
            return _userManager.FindByNameAsync(User.Identity.Name).Result;
        }

        #endregion  
    }
}
