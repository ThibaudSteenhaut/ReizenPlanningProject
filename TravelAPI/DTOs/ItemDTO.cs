using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.DTOs
{
    public class ItemDTO
    {

        #region Methods
        public string Name { get; set; }
        
        public string Category { get; set; }

        public int Amount { get; set; } = 1;
        #endregion

        #region Constructors
        public ItemDTO()
        {
        }

        public ItemDTO(string name, string category, int amount)
        {
            Name = name;
            Category = category;
            Amount = amount;
        }
        #endregion  
    }
}
