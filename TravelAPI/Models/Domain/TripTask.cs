using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
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

        public TripTask(string description, bool done = false)
        {
            Description = description;
            Done = done;
        }

        #endregion

    }
}
