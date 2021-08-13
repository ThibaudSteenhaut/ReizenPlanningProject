using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class TripItemDTO
    {

        #region Properties

        public int Id { get; set; }
        public bool CheckedIn { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public TripCategoryDTO Category { get; set; }

        #endregion

        #region Constructors

        public TripItemDTO()
        {

        }

        public TripItemDTO(TripItem tripItem)
        {
            Id = tripItem.Id;
            CheckedIn = tripItem.CheckedIn;
            Amount = tripItem.Amount;
            Name = tripItem.Name; 
            Category = new TripCategoryDTO(tripItem.Category);
        }

        #endregion
    }
}
