using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
{
    public class Category
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public IdentityUser User { get; set; }
        public bool IsGeneralCategory { get; set; }

        //if the Category is not a general category but only used for a certain trip 
        public Trip Trip { get; set; }

        public Category()
        {

        }

        public Category(string name, IdentityUser user, bool isGeneralCategory, Trip trip = null)
        {
            Name = name;
            User = user;
            IsGeneralCategory = isGeneralCategory;
            Trip = trip; 
        }
    }
}
