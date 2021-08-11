using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
       
        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
