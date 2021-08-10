using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel;
using ReizenPlanningProject.Views.Trips;
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

namespace ReizenPlanningProject
{

    public sealed partial class TripsOverviewPage : Page
    {

        private TripOverviewViewModel _tripOverviewViewModel { get; set; }
         
        public TripsOverviewPage()
        {
            this.InitializeComponent();
            this._tripOverviewViewModel = new TripOverviewViewModel();
            this.DataContext = _tripOverviewViewModel;
        }

        private async void DeleteTrip_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            Trip trip = btn.DataContext as Trip;

            ContentDialogResult result = await ShowDeleteDialogAsync(trip.Destination);

            if(result == ContentDialogResult.Primary)
            {
                _tripOverviewViewModel.DeleteCommand.Execute(trip);
            }
        }

        private async Task<ContentDialogResult> ShowDeleteDialogAsync(string destination)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Delete trip?",
                Content = $"Are you sure you want to delete the trip to {destination}?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            return await dialog.ShowAsync();
        }

        private void tripLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(TripDetailPage), tripLv.SelectedItem);
        }
    }
}
