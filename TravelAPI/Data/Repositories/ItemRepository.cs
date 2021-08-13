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
        private readonly DbSet<GeneralItem> _items;
        private readonly DbSet<GeneralCategory> _categories;
        private readonly DbSet<Trip> _trips;
        private readonly DbSet<TripItem> _tripItems;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _items = context.Items;
            _categories = context.Categories;
            _trips = context.Trips;
            _tripItems = context.TripItems;
        }

        #region General Items

        public IEnumerable<GeneralItem> GetGeneralItems(string userId)
        {
            return _items.Include(i => i.Category).Where(i => i.User.Id == userId).OrderBy(i => i.Category).ThenBy(i => i.Name);
        }

        public GeneralItem GetBy(int itemId)
        {
            return _items.FirstOrDefault(i => i.Id == itemId);
        }

        public void Add(GeneralItem item)
        {
            _items.Attach(item);
        }

        public void Update(GeneralItem item)
        {
            _context.Update(item);
        }

        public void Delete(GeneralItem item)
        {
            _items.Remove(item);
        }

        #endregion


        #region General Category

        public GeneralCategory GetCategoryBy(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public IEnumerable<GeneralCategory> GetCategories(string userId)
        {
            return _categories.Where(c => c.User.Id == userId).OrderBy(c => c.Name);
        }

        public IEnumerable<GeneralCategory> GetTripCategories(string userId, int tripId)
        {
            return _categories.Where(c => c.User.Id == userId).OrderBy(c => c.Name);
        }

        public void AddCategory(GeneralCategory category)
        {
            _categories.Add(category);
        }

        public void DeleteCategoryWithItems(GeneralCategory category)
        {
            _items.RemoveRange(_items.Include(i => i.Category).Where(i => i.Category.Id == category.Id));
            _categories.Remove(category);
        }

        #endregion


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
