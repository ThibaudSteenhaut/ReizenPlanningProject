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
    public class GeneralItem
    {

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public GeneralCategory Category { get; set; }
        public IdentityUser User { get; set; }

        #endregion

        #region Constructor

        public GeneralItem()
        {

        }

        public GeneralItem(string name, GeneralCategory category, IdentityUser user)
        {
            Name = name;
            Category = category;
            User = user;
        }

        public GeneralItem(GeneralItemDTO item, GeneralCategory category, IdentityUser user)
        {
            Name = item.Name;
            Category = category;
            User = user;
        }
        
        #endregion
    }
}
