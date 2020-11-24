using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
            //this.Trips = new ObservableCollection<Trip>(DummyDataSource.Trips);


            Trips = new ObservableCollection<Trip>();

            HaalDataOp();
        }


        private async void HaalDataOp()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("https://localhost:44316/api/trip"));
            var lst = JsonConvert.DeserializeObject<ObservableCollection<Trip>>(json);
            foreach (Trip t in lst)
            {
                this.Trips.Add(t);
            }
        }

    }
}
