using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class ItineraryItemDTO
    {

        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int DaysRemaining { get; set; }

        #endregion

        #region Constructor

        public ItineraryItemDTO()
        {

        }

        public ItineraryItemDTO(ItineraryItem itineraryItem)
        {
            Description = itineraryItem.Description;
            Date = itineraryItem.Date;
            DaysRemaining = (itineraryItem.Date - DateTime.Now).Days;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion


    }
}
