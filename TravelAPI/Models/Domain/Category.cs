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

        public Category()
        {

        }
        
        public Category(string name, IdentityUser user)
        {
            Name = name;
            User = user;
        }
    }
}
