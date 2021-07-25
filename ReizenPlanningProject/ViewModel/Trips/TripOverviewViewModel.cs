using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ReizenPlanningProject.Model.Repositories;
using System.Diagnostics;

namespace ReizenPlanningProject.ViewModel
{
    public class TripOverviewViewModel : INotifyPropertyChanged
    {

        #region Fields 

        private readonly ITripRepository _tripRepository = new TripRepository();
        private bool _isProgressRingActive = false;

        #endregion

        #region Properties 

        public ObservableCollection<Trip> Trips { get; private set; } = new ObservableCollection<Trip>();

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsProgressRingActive
        {
            get { return this._isProgressRingActive; }
            set
            {
                this._isProgressRingActive = value;
                this.RisePropertyChanged(nameof(this.IsProgressRingActive));
            }
        }

        #endregion

        #region Commands 

        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand AddTripCommand { get; set; }

        #endregion

        #region Constructors 

        public TripOverviewViewModel()
        {

            DeleteCommand = new RelayCommand(DeleteTrip); 
            GetTrips();
        }

        #endregion

        #region Methods 

        private void GetTrips()
        {

            this.IsProgressRingActive = true;

            this.Trips = _tripRepository.GetTrips();

            this.IsProgressRingActive = false;

        }

        public void DeleteTrip(object obj)
        {

            Trip trip = obj as Trip;
            Trips.Remove(trip);
            _tripRepository.Remove(trip.Id); 
        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }

        #endregion
    }
}