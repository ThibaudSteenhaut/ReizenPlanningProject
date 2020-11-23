using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public class TripItem
    {

        #region Fields
        public int TripId { get; set; }
        public int ItemId { get; set; }

        public int Amount { get; set; }

        public Trip Trip { get; set; }
        public Item Item { get; set; }
        //public Category Category { get; set; }
        #endregion

        #region Constructors

        public TripItem()
        {

        }

        public TripItem(int tripId, int itemId)
        {
            TripId = tripId;
            ItemId = itemId;
        }
        #endregion
    }
}
