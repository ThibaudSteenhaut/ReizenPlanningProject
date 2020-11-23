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
        [Required]
        public string Name { get; set; }
        
        #endregion

        #region Constructors
        public ItemDTO()
        {
        }

        public ItemDTO(Item item)
        {
            Name = item.Name;
        }
        #endregion  
    }
}
