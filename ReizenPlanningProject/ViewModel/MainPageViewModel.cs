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
        public ObservableCollection<Reis> Reizen { get; set; }


        public MainPageViewModel()
        {
            this.Reizen = new ObservableCollection<Reis>(DummyDataSource.Reizen);

        }


       
    
}
}
