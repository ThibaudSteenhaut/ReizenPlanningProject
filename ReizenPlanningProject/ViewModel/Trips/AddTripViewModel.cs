using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ReizenPlanningProject.ViewModel.Trips
{
    public class AddTripViewModel
    {

        #region Fields 

        private readonly TripRepository _tripRepository = new TripRepository();

        #endregion

        #region Properties 

        public string Destination { get; set; }

        public DateTimeOffset DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }

        public DateTimeOffset ReturnDate { get; set; }
        public TimeSpan ReturnTime { get; set; }

        #endregion

        #region Commands

        public RelayCommand AddTripCommand { get; set; }

        #endregion

        #region Constructors

        public AddTripViewModel()
        {

            AddTripCommand = new RelayCommand(parm => AddTrip());
            DepartureDate = DateTime.Now;
            DepartureTime = DateTime.Now.TimeOfDay;
            ReturnDate = DepartureDate.AddDays(1); 
        }

        #endregion

        #region Methods

        public async void AddTrip()
        {
            if(!String.IsNullOrEmpty(Destination))
            {
                DateTime departureDt = new DateTime(
                    DepartureDate.Year, 
                    DepartureDate.Month, 
                    DepartureDate.Month, 
                    DepartureTime.Hours, 
                    DepartureTime.Minutes, 
                    DepartureTime.Seconds);
              
                DateTime returnDt = new DateTime(
                    ReturnDate.Year,
                    ReturnDate.Month,
                    ReturnDate.Month,
                    ReturnTime.Hours,
                    ReturnTime.Minutes,
                    ReturnTime.Seconds);

                await _tripRepository.Add(new Trip(Destination, departureDt, returnDt));
                NavigateToListOverview();
              
            }

        }


        private void NavigateToListOverview()
        {
            Frame frame = (Frame)Window.Current.Content;
            frame.Navigate(typeof(MainPage), "TripList");
        }
        #endregion
    }
}
