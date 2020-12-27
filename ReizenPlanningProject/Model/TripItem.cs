using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class TripItem
    {
        public Trip Trip { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Boolean IsChecked { get; set; }

        public TripItem(Trip trip, Item item)
        {
            Trip = trip;
            Item = item;
            Amount = 0;
            IsChecked = false;
        }
    }
}
