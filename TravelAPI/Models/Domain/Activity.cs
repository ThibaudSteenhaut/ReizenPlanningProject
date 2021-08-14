using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models.Domain
{
    public class Activity
    {

        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Day { get; set; }

        #endregion

        #region Constructor

        public Activity()
        {

        }

        public Activity(string description, DateTime day)
        {
            Description = description;
            Day = day;
        }

        #endregion

    }
}
