using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int Name { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
