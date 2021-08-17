using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain
{
    public class TripTask
    {
        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

        #endregion

        #region Constructor

        public TripTask()
        {

        }

        public TripTask(string description, bool done)
        {
            Description = description;
            Done = done;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
