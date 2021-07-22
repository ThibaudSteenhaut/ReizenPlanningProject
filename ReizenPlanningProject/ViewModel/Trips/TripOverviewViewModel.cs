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

        private readonly ITripRepository _tripRepository = new TripRepository();

        public ObservableCollection<Trip> Trips { get; private set; } = new ObservableCollection<Trip>();

        public RelayCommand DeleteCommand { get; private set; }
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

            DeleteCommand = new RelayCommand(DeleteTrip); 
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
    }
}