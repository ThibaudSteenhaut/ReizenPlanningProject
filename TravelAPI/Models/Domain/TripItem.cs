﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public class TripItem
    {

        #region Properties
        public int Id { get; set; }
        
        public bool CheckedIn { get; set; }

        public int Amount { get; set; }

        public Item Item { get; set; }

        #endregion

        #region Constructors

        public TripItem()
        {

        }

        public TripItem(Item item, int amount, bool checkedIn)
        {
            Item = item;
            Amount = amount;
            CheckedIn = CheckedIn;
        }
        #endregion
    }
}
