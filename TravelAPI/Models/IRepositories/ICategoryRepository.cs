using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public interface ICategoryRepository
    {
        Category GetBy(int id);
        IEnumerable<Category> GetAll();
        Item GetItemBy(int categoryId, int itemId);
        void Add(Category category);
        void Delete(Category category);
        void Update(Category category);
        void SaveChanges();
    }
}
