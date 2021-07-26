using Microsoft.AspNetCore.Identity;
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

        #region Properties

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public string Category { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        #endregion

        #region Constructor

        public Item()
        {

        }

        public Item(string name, string category, IdentityUser user)
        {
            Name = name;
            Category = category;
            User = user;
        }

        public Item(ItemDTO dto, IdentityUser user)
        {
            Name = dto.Name;
            User = user; 
        }
        
        #endregion
    }
}
