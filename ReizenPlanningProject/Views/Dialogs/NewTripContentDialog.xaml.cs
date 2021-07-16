using ReizenPlanningProject.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReizenPlanningProject
{
    public sealed partial class NewTripContentDialog : ContentDialog
    {

        public string DestinationName 
        {
            get { return DestinationNameBox.Text; }
            set { DestinationNameBox.Text = value; } 
        }

        public DateTime DepartureDate
        {
            get { return DepartureDatePicker.SelectedDate.GetValueOrDefault().UtcDateTime; }
        }

        public DateTime ReturnDate
        {
            get { return ReturnDatePicker.SelectedDate.GetValueOrDefault().UtcDateTime; }
        }

        public TimeSpan DepartureTime
        {
            get { return DepartureHourPicker.SelectedTime.GetValueOrDefault(); }
        }

        public TimeSpan ReturnTime
        {
            get { return ReturnHourPicker.SelectedTime.GetValueOrDefault(); }
        }


        public NewTripContentDialog()
        {
            this.InitializeComponent();
            Trip newTrip = new Trip();
            this.DataContext = newTrip; 
        }
        

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //add new trip
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}
