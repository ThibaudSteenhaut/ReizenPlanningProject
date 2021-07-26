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

        #endregion

        #region Constructors
        public ItemDTO() { }

        public ItemDTO(Item item)
        {
            Name = item.Name;
            Category = item.Category;
        }

        public ItemDTO(string name, string category)
        {
            Name = name;
            Category = category;
        }
        #endregion  
    }
}
