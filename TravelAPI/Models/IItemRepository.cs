using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public interface IItemRepository
    { 
        Item GetBy(int id);
        void SaveChanges();
    }
}
