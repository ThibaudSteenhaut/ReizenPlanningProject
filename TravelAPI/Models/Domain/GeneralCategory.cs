using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
{
    public class GeneralCategory
    {

        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }
        public IdentityUser User { get; set; }

        #endregion

        #region Constructors 

        public GeneralCategory()
        {

        }

        public GeneralCategory(string name, IdentityUser user)
        {
            Name = name;
            User = user;
        }

        #endregion
    }
}
