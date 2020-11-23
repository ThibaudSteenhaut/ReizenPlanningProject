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

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(Category category)
        {
            _categories.Remove(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.Include(c => c.Items);
        }

        public Category GetBy(int id)
        {
            return _categories.Include(c => c.Items).FirstOrDefault(c => c.Id == id); 
        }

        public Item GetItemBy(int categoryId, int itemId)
        {
            return _categories.SingleOrDefault(c => c.Id == categoryId).Items.SingleOrDefault(i => i.Id == itemId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _categories.Update(category);
        }
        #endregion



    }
}
