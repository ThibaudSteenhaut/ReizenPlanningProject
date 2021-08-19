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

        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion


        #region Constructors 

        public Category()
        {

        }

        #endregion

        #region Methods 

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
