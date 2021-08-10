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
        private readonly DbSet<Trip> _trips;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _items = context.Items;
            _categories = context.Categories;
            _trips = context.Trips; 
        }

        public IEnumerable<Item> GetGeneralItems(string userId)
        {
            return _items.Include(i => i.Category).Where(i => i.User.Id == userId && i.IsGeneralItem == true).OrderBy(i => i.Category).ThenBy(i => i.Name);
        }

        public Item GetBy(int itemId)
        {
            return _items.FirstOrDefault(i => i.Id == itemId);
        }

        public Category GetCategoryBy(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _categories.Where(c => c.User.Id == userId).OrderBy(c => c.Name);
        }

        public IEnumerable<TripItem> GetTripItems(int tripId)
        {
            return _trips.Include(tr => tr.TripItems).ThenInclude(ti => ti.Item).ThenInclude(i => i.Category).SingleOrDefault(t => t.Id == tripId).TripItems; 
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
