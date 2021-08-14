using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain
{
    public class GroupItemList : List<Item>
    {
        public GroupItemList(IEnumerable<Item> items) : base(items)
        {

        }

        public string Key { get; set; }
    }
}
