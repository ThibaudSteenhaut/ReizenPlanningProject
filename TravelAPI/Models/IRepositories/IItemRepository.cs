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
        IEnumerable<Item> GetItems(string userId);
        IEnumerable<Category> GetCategories(string userId);
        void AddCategory(Category category);
        void Add(Item item); 
        void SaveChanges();
    }
}
