using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
namespace ReizenPlanningProject.ViewModel
{
    class DetailsPageViewModel
    {
       
        public Trip Trip { get; set; }

        public DetailsPageViewModel(Trip trip)
        {
            this.Trip = trip;
        }

       
    }
}
