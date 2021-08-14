using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain
{
    public class TripItem
    {
        #region Properties

        public int Id { get; set; }
        public bool CheckedIn { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }

        #endregion

        #region Constructors

        public TripItem()
        {

        }

        public TripItem(string name, Category tripCategory, int amount, bool checkedIn = false)
        {
            Name = name;
            Category = tripCategory;
            Amount = amount;
            CheckedIn = CheckedIn;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}
