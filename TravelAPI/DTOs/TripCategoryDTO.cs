using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class TripCategoryDTO
    {

        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion


        #region Constructors 

        public TripCategoryDTO()
        {

        }

        public TripCategoryDTO(TripCategory category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        #endregion

    }
}
