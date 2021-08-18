using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class ItineraryItem
    {

        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int DaysRemaining { get; set; }

        #endregion

        #region Constructor

        public ItineraryItem()
        {

        }

        public ItineraryItem(string description, DateTime date, int daysRemaining = 0)
        {
            Description = description;
            Date = date;
            DaysRemaining = daysRemaining;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion

    }
}
