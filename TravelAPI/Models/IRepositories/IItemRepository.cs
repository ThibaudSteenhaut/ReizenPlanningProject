using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetGeneralItems(string userId);
        Item GetBy(int itemId);
        IEnumerable<Category> GetCategories(string userId);
        IEnumerable<TripItem> GetTripItems(int tripId); 
        Category GetCategoryBy(int categoryId);
        void AddCategory(Category category);
        void Add(Item item);
        void Delete(Item item);
        void SaveChanges();
    }
}
