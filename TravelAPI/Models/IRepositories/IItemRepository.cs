using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems(string userId);
        void SaveChanges();
    }
}
