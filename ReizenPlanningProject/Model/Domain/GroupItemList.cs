using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain
{
    public class GroupItemList : List<object>
    {
        public GroupItemList(IEnumerable<object> items) : base(items)
        {

        }

        public object Key { get; set; }
    }
}
