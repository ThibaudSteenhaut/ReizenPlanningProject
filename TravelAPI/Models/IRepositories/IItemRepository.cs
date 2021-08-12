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
        IEnumerable<Category> GetGeneralCategories(string userId);
        IEnumerable<Category> GetTripCategories(string userId, int tripId);
        IEnumerable<TripItem> GetTripItems(int tripId); 
        Category GetCategoryBy(int categoryId);
        void Update(Item item);
        void AddCategory(Category category);
        void DeleteCategoryWithItems(Category category); 
        void Add(Item item);
        void Delete(Item item);
        void SaveChanges();
    }
}
