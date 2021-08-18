using ReizenPlanningProject.Model;
using System;
using System.Collections.Generic;
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


namespace ReizenPlanningProject.Views.Dialogs
{
    public sealed partial class AddItineraryItemDialog : ContentDialog
    {

        public Trip Trip { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime { get; set; }
        public string Description { get; set; }

        public AddItineraryItemDialog(Trip trip)
        {
            this.InitializeComponent();
            this.Trip = trip;
            this.DataContext = this;
            SelectedDate = trip.DepartureDate;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
