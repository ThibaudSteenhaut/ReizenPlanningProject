using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        //POST: api/Items
        /// <summary>
        /// Add a general item to the logged in user 
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "User")]
        public ActionResult<ItemDTO> AddItem(ItemDTO item)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            Item itemToCreate = new Item(item, item.Category, currentUser);
            _itemRepository.Add(itemToCreate);
            _itemRepository.SaveChanges();

            return Ok(itemToCreate.Id);

        }

        //DELETE: api/Items/{id} 
        /// <summary> 
        /// Delete an item 
        /// </summary> 
        /// <param name="id">The id of the item to delete</param> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "User")]
        public ActionResult<Trip> DeleteItem(int id)
        {

            if (id < 0)
                return BadRequest("Not a valid item id");

            Item item = _itemRepository.GetBy(id);

            if (item == null)
                return NotFound();

            _itemRepository.Delete(item);
            _itemRepository.SaveChanges();
            return Ok();
        }

        //GET: api/Items/Categories
        /// <summary> 
        /// Gets the categories of the logged in user
        /// </summary> 
        [HttpGet("categories")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            IEnumerable<CategoryDTO> categories = _itemRepository.GetCategories(GetCurrentUser().Id).Select(c => new CategoryDTO(c));

            return Ok(categories);
        }


        //POST: api/Items
        /// <summary>
        /// Add a general item to the logged in user 
        /// </summary>
        [HttpPost("category")]
        [Authorize(Policy = "User")]
        public ActionResult<int> AddCategory(Category category)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null || String.IsNullOrEmpty(category.Name))
            {
                return BadRequest();
            }

            Category categoryToCreate = new Category(category.Name, currentUser); 
            _itemRepository.AddCategory(categoryToCreate);
            _itemRepository.SaveChanges();

            return Ok(categoryToCreate.Id);

        }


        //PUT: api/Items
        /// <summary>
        /// Updates the items of the logged in user
        /// </summary>
        [HttpPut]
        [Authorize(Policy = "User")]
        public ActionResult UpdateItems(List<ItemDTO> items)
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            //items.ForEach(i => _itemRepository.Update(i));
            _itemRepository.SaveChanges();

            return Ok();
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
