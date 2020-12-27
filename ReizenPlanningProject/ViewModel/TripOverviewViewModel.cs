using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ReizenPlanningProject.Model.Repositories;

namespace ReizenPlanningProject.ViewModel
{
    public class TripOverviewViewModel : INotifyPropertyChanged
    {

        private readonly ITripRepository _tripRepository = new TripRepository();

        public ObservableCollection<Trip> Trips { get; set; } = new ObservableCollection<Trip>();

        public RelayCommand AddTripCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isProgressRingActive = false;
        public bool IsProgressRingActive
        {
            get { return this.isProgressRingActive; }
            set
            {
                this.isProgressRingActive = value;
                this.RisePropertyChanged(nameof(this.IsProgressRingActive));
            }
        }

        public TripOverviewViewModel()
        {
            //this.Trips = new ObservableCollection<Trip>(DummyDataSource.Trips);
            //AddTripCommand = new RelayCommand((param) => AddTripAsync(param));
            GetTrips();

        }

        
        private void GetTrips()
        {

            this.IsProgressRingActive = true;

            this.Trips = _tripRepository.GetTrips();

            this.IsProgressRingActive = false;

        }

        public async void AddTrip(string destination, DateTime departureDate, DateTime returnDate)
        {

            Trip tripToAdd = new Trip { Destination = destination, DepartureDate = departureDate, ReturnDate = returnDate };

            //TODO geef positieve feedback als correct toegevoegd is
            bool succes = await _tripRepository.Add(tripToAdd);
            this.Trips.Add(tripToAdd);

        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}