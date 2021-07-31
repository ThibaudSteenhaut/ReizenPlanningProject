using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class Item
    {
        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }

        #endregion

        #region Constructor

        public Item()
        {

        }

        public Item(string name, Category category)
        {
            Name = name;
            Category = category;
        }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this); 
        }
    }
}
