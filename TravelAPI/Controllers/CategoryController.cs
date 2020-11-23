using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAPI.DTOs;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{

    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;
        }

        //GET: api/Categories
        /// <summary> 
        /// Get all categories 
        /// </summary> 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(_categoryRepository.GetAll().Select(c => new CategoryDTO(c)));
        }


        //GET: api/Categories/{id} 
        /// <summary> 
        /// Get a trip 
        /// </summary> 
        /// <param name="id">The id of the categorie</param> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<TripDTO> GetCategory(int id)
        {

            Category category = _categoryRepository.GetBy(id);
            if (category == null)
                return NotFound();
            return Ok(new CategoryDTO(category));
        }

        //POST: api/Category 
        /// <summary> 
        /// Add a new category
        /// </summary> 
        /// <param name="categoryDTO">The category to add</param> 
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Item> PostCategory(CategoryDTO categoryDTO)
        {

            if (categoryDTO == null) return BadRequest();

            Category catToCreate = new Category(categoryDTO);
            _categoryRepository.Add(catToCreate);
            _categoryRepository.SaveChanges();
            return Ok(catToCreate);

        }

        //DELETE: api/Category/{id} 
        /// <summary> 
        /// Delete a category 
        /// </summary> 
        /// <param name="id">The id of the category to delete</param> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<Item> DeleteCategory(int id)
        {

            if (id < 0) return BadRequest("Not a valid category id");

            Category category = _categoryRepository.GetBy(id);

            if (category == null) return NotFound();

            _categoryRepository.Delete(category);
            _categoryRepository.SaveChanges();
            return Ok(category);
        }


        //POST: api/Category/{id}/Item
        /// <summary> 
        /// Add a new item to the category
        /// </summary> 
        /// <param name="id">the id of the category</param>
        [HttpPost("{id}/Item")]
        [AllowAnonymous]
        public ActionResult<Item> AddItem(int id, ItemDTO itemDTO)
        {

            if (itemDTO == null) return BadRequest();

            Item itemToCreate = new Item(itemDTO);
            Category category = _categoryRepository.GetBy(id);
            category.AddItem(itemToCreate);
            _categoryRepository.SaveChanges();
            return Ok(itemToCreate);

        }


        ///DELETE: api/Category/{id}/Item/{id}
        ///<summary>
        /// Deletes the item from a category
        /// Delete is on cascade so any item deleted from a category will be deleted 
        /// from a trip as well
        /// </summary>
        /// <param name="id">the category id</param>
        /// <param name="itemId">the item id</param>
        [HttpDelete("{id}/Item/{itemId}")]
        [AllowAnonymous]
        public ActionResult<Item> DeleteItem(int id, int itemId)
        {

            if (id < 0) return BadRequest();
            if (itemId < 0) return BadRequest();

            Category category = _categoryRepository.GetBy(id);
            Item itemToDelete = _categoryRepository.GetItemBy(id, itemId);
            category.RemoveItem(itemToDelete);
            _categoryRepository.SaveChanges();
            return Ok(itemToDelete);
        }
    }
}
