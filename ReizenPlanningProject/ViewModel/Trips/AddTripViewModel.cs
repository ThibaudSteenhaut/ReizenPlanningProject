﻿using ReizenPlanningProject.Model;
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

        public DateTimeOffset Today = new DateTimeOffset(DateTime.Now.ToUniversalTime());
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }

        #endregion

        #region Commands

        public RelayCommand AddTripCommand { get; set; }

        #endregion

        #region Constructors

        public AddTripViewModel()
        {
            AddTripCommand = new RelayCommand(parm => AddTrip());
            DepartureDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ReturnDate = DepartureDate.AddDays(7); 
        }

        #endregion

        #region Methods

        public async void AddTrip()
        {
            if(!String.IsNullOrEmpty(Destination))
            {
                await _tripRepository.Add(new Trip(Destination, DepartureDate, ReturnDate, 0));
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
