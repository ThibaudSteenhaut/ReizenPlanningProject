using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _categories;
        #endregion

        #region Constructor 
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _categories = context.Categories;
        }
        #endregion



        public void Add(Category category)
        {
            _categories.Add(category); 
        }

        public void AddItem(int id, Item item)
        {
            _categories.SingleOrDefault(c => c.Id == id).AddItem(item);
        }

        public void Delete(Category category)
        {
            _categories.Remove(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.OrderByDescending(c => c.Name).ToList(); 
        }

        public Category GetBy(int id)
        {
            return _categories.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Item> GetItems(int id)
        {
            return _categories.SingleOrDefault(c => c.Id == id).Items;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _categories.Update(category);
        }
    }
}
