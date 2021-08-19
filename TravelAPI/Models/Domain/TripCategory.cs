using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
{
    public class TripCategory
    {

        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }
        public Trip Trip { get; set; }

        #endregion

        #region Constructors 

        public TripCategory()
        {

        }

        public TripCategory(string name, Trip trip)
        {
            Name = name;
            Trip = trip;
        }

        #endregion

    }
}
