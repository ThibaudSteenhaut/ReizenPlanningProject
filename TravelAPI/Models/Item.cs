using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public class Item
    {

        #region Fields
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        [Required]
        public int Amount { get; set; }
        #endregion

        #region Constructor

        public Item()
        {

        }

        public Item(string name, int amount)
        {
            Name = name;
            Amount = amount; 
        }
        
        #endregion
    }
}
