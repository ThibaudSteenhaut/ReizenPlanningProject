using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Item> _items;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _items = context.Items;
        }
        public Item GetBy(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
