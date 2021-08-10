using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public class Item
    {

        #region Properties

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public Category Category { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public bool IsGeneralItem { get; set; }

        #endregion

        #region Constructor

        public Item()
        {

        }

        public Item(string name, Category category, IdentityUser user, bool isGeneralItem)
        {
            Name = name;
            Category = category;
            User = user;
            IsGeneralItem = isGeneralItem;
        }

        public Item(ItemDTO item, Category category, IdentityUser user)
        {
            Name = item.Name;
            IsGeneralItem = item.IsGeneralItem;
            Category = category;
            User = user;
        }
        
        #endregion
    }
}
