using ReizenPlanningProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel
{
    class MainPageViewModel
    {
        //Data
        public ObservableCollection<Trip> Trips { get; set; }


        public MainPageViewModel()
        {
            this.Trips = new ObservableCollection<Trip>(DummyDataSource.Trips);

        }
    }
}
