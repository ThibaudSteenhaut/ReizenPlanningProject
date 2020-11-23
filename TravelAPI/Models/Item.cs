using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;

namespace TravelAPI.Models
{
    public class Item
    {

        #region Fields
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<TripItem> TripItems { get; set; }

        #endregion

        #region Constructor

        public Item()
        {

        }

        public Item(string name, int amount)
        {
            Name = name;
        }

        public Item(ItemDTO dto)
        {
            Name = dto.Name;
        }
        
        #endregion
    }
}
