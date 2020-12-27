using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }


        public override string ToString()
        {
            return String.Concat($"id: {Id}, Name: {Name}, Category: {Category}");
        }
    }
}
