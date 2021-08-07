using ReizenPlanningProject.ViewModel.Trips;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ReizenPlanningProject.Views.Trips
{

    public sealed partial class AddTripPage : Page
    {
        public AddTripViewModel _addTripVM { get; set; }

        public AddTripPage()
        {
            this.InitializeComponent();
            _addTripVM = new AddTripViewModel();
            this.DataContext = _addTripVM; 
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

            if(DepartureDate.Date > ReturnDate.Date)
            {
                ReturnDate.Date = DepartureDate.Date.Value.AddDays(7);
            }
        }
    }
}
