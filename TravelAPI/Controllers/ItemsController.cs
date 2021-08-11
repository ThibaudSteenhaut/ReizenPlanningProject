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
        /// Gets the general items of the logged in user
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

            IEnumerable<ItemDTO> items = _itemRepository.GetGeneralItems(GetCurrentUser().Id).Select(i => new ItemDTO(i));

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

            Category category = _itemRepository.GetCategoryBy(item.Category.Id);
            Item itemToCreate = new Item(item, category, currentUser);
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
        [HttpGet("GeneralCategories")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<CategoryDTO>> GetGeneralCategories()
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return BadRequest();
            }

            IEnumerable<CategoryDTO> categories = _itemRepository.GetGeneralCategories(GetCurrentUser().Id).Select(c => new CategoryDTO(c));

            return Ok(categories);
        }


        //POST: api/Items
        /// <summary>
        /// Add a general item to the logged in user 
        /// </summary>
        [HttpPost("category")]
        [Authorize(Policy = "User")]
        public ActionResult<int> AddGeneralCategory(CategoryDTO category)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null || String.IsNullOrEmpty(category.Name))
            {
                return BadRequest();
            }

            Category categoryToCreate = new Category(category.Name, currentUser, false); 
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
