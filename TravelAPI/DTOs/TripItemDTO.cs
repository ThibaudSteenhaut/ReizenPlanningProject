﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.DTOs
{
    public class TripItemDTO
    {

        #region Properties
        public int Id { get; set; }

        public bool CheckedIn { get; set; }

        public int Amount { get; set; }

        public ItemDTO Item { get; set; }

        #endregion

        public TripItemDTO()
        {

        }

        public TripItemDTO(TripItem tripItem)
        {
            Id = tripItem.Id;
            CheckedIn = tripItem.CheckedIn;
            Amount = tripItem.Amount;
            Item = new ItemDTO(tripItem.Item);
        }
    }
}
