using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel.Trips
{
    public class PastTripsViewModel
    {

        #region Fields

        private readonly ITripRepository _tripRepository = new TripRepository();

        #endregion

        #region Properties

        public ObservableCollection<Trip> Trips { get; set; }

        #endregion


        #region Constructor

        public PastTripsViewModel()
        {

            Trips = _tripRepository.GetPastTrips(); 
        }

        #endregion

    }
}
