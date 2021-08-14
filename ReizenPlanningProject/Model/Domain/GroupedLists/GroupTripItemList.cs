using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain
{
    public class GroupTripItemList : List<TripItem>
    {
        public GroupTripItemList(IEnumerable<TripItem> items) : base(items)
        {

        }

        public string Key { get; set; }
    }
}
