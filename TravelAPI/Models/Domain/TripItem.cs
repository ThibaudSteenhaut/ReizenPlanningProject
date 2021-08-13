using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public class TripItem
    {

        #region Properties

        public int Id { get; set; }
        public bool CheckedIn { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public TripCategory Category { get; set; }

        #endregion

        #region Constructors

        public TripItem()
        {

        }

        public TripItem(string name, int amount, TripCategory category, bool checkedIn)
        {
            Name = name;
            Category = category; 
            Amount = amount;
            CheckedIn = CheckedIn;
        }
        #endregion
    }
}
