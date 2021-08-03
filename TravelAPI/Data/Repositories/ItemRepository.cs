using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Item> _items;
        private readonly DbSet<Category> _categories;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _items = context.Items;
            _categories = context.Categories;
        }

        public IEnumerable<Item> GetItems(string userId)
        {
            return _items.Include(i => i.Category).Where(i => i.User.Id == userId).OrderBy(i => i.Category).ThenBy(i => i.Name);
        }

        public Item GetBy(int itemId)
        {
            return _items.FirstOrDefault(i => i.Id == itemId);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _categories.Where(c => c.User.Id == userId).OrderBy(c => c.Name);
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void Add(Item item)
        {
            _items.Attach(item); 
        }


        public void Delete(Item item)
        {
            _items.Remove(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
