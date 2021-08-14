﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Domain.GroupedLists
{
    public class GroupActivityList : List<Activity>
    {
        public GroupActivityList(IEnumerable<Activity> activities) : base(activities)
        {

        }

        public DateTime Key { get; set; }
    }
}
