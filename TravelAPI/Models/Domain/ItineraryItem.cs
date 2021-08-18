using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
{
    public class ItineraryItem
    {

        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        #endregion

        #region Constructor

        public ItineraryItem()
        {

        }

        public ItineraryItem(string description, DateTime date)
        {
            Description = description;
            Date = date;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
