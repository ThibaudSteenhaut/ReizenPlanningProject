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

        #region Fields 

        private readonly ITripRepository _tripRepository;
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion

        #region Constructor 

        public ItemsController(ITripRepository tripRepo, IItemRepository itemRepo, UserManager<IdentityUser> userManager)
        {

            _tripRepository = tripRepo;
            _itemRepository = itemRepo;
            _userManager = userManager;
        }

        #endregion

        #region Methods 

        #region Items

        //GET: api/Items
        /// <summary> 
        /// Gets the general items of the logged in user
        /// </summary> 
        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<GeneralItemDTO>> GetItems()
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            IEnumerable<GeneralItemDTO> items = _itemRepository.GetGeneralItems(GetCurrentUser().Id).Select(i => new GeneralItemDTO(i));

            return Ok(items);
        }

        //POST: api/Items
        /// <summary>
        /// Add a general item to the logged in user 
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "User")]
        public ActionResult<GeneralItemDTO> AddItem(GeneralItemDTO item)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            GeneralCategory category = _itemRepository.GetCategoryBy(item.Category.Id);
            GeneralItem itemToCreate = new GeneralItem(item, category, currentUser);
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
        [Authorize(Policy = "User")]
        public ActionResult<Trip> DeleteItem(int id)
        {

            if (id < 0)
                return NotFound();

            GeneralItem item = _itemRepository.GetBy(id);

            if (item == null)
                return NotFound();

            _itemRepository.Delete(item);
            _itemRepository.SaveChanges();
            return Ok();
        }

        #endregion

        #region Categories 

        //GET: api/Items/Categories
        /// <summary> 
        /// Gets the general categories of the logged in user
        /// </summary> 
        [HttpGet("Categories")]
        [Authorize(Policy = "User")]
        public ActionResult<IEnumerable<GeneralCategoryDTO>> GetGeneralCategories()
        {
            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();

            IEnumerable<GeneralCategoryDTO> categories = _itemRepository.GetCategories(GetCurrentUser().Id).Select(c => new GeneralCategoryDTO(c));

            return Ok(categories);
        }


        //POST: api/Items/Categories
        /// <summary>
        /// Add a general category to the logged in user 
        /// </summary>
        [HttpPost("Category")]
        [Authorize(Policy = "User")]
        public ActionResult<int> AddCategory(GeneralCategoryDTO category)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null || String.IsNullOrEmpty(category.Name))
            {
                return BadRequest();
            }

            GeneralCategory categoryToCreate = new GeneralCategory(category.Name, currentUser);
            _itemRepository.AddCategory(categoryToCreate);
            _itemRepository.SaveChanges();

            return Ok(categoryToCreate.Id);

        }

        //DELETE: api/Items/Category/{id}
        /// <summary>
        /// Delete a general category for the logged in user, all relating general items will be deleted as well
        /// </summary>
        [HttpDelete("Category/{id}")]
        [Authorize(Policy = "User")]
        public ActionResult DeleteGeneralCategory(int id)
        {

            IdentityUser currentUser = GetCurrentUser();

            if (currentUser == null) return BadRequest();
            if (id < 0) return NotFound();

            GeneralCategory categoryToDelete = _itemRepository.GetCategoryBy(id);
            _itemRepository.DeleteCategoryWithItems(categoryToDelete);
            _itemRepository.SaveChanges();

            return Ok();

        }

        private IdentityUser GetCurrentUser()
        {
            return _userManager.FindByNameAsync(User.Identity.Name).Result;
        }

        #endregion

        #endregion
    }
}
