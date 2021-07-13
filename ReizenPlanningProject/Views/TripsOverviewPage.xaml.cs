using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel;
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

namespace ReizenPlanningProject
{
    /// <summary>
    /// The page that will display the planned trips 
    /// </summary>
    public sealed partial class TripsOverviewPage : Page
    {

        private TripOverviewViewModel _tripOverviewViewModel { get; set; }
         
        public TripsOverviewPage()
        {
            this.InitializeComponent();
            this._tripOverviewViewModel = new TripOverviewViewModel();
            this.DataContext = _tripOverviewViewModel;
        }

        private async void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            NewTripContentDialog dialog = new NewTripContentDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                DateTime dep = new DateTime(dialog.DepartureDate.Year, dialog.DepartureDate.Month, dialog.DepartureDate.Day, dialog.DepartureTime.Hours, dialog.DepartureTime.Minutes, 0);
                DateTime ret = new DateTime(dialog.ReturnDate.Year, dialog.ReturnDate.Month, dialog.ReturnDate.Day, dialog.ReturnTime.Hours, dialog.ReturnTime.Minutes, 0);
                _tripOverviewViewModel.AddTrip(dialog.DestinationName, dep, ret);
            }
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip selectedTrip = (Trip)tripLv.SelectedItem;
            Frame.Navigate(typeof(DetailsPage), selectedTrip);
        }

        private void MenuFlyout_Opening(object sender, object e)
        {
            MenuFlyout senderAsMenuFlyout = sender as MenuFlyout;

            foreach (object menuFlyoutItem in senderAsMenuFlyout.Items)
            {
                if (menuFlyoutItem.GetType() == typeof(MenuFlyoutItem))
                {
                    // Associate the particular FeedItem with the menu flyout (so the MenuFlyoutItem knows which FeedItem to act upon)
                    ListViewItem itemContainer = senderAsMenuFlyout.Target as ListViewItem;

                    //var data = favoriteListView.ItemFromContainer(itemContainer);

                    //(menuFlyoutItem as MenuFlyoutItem).CommandParameter = data;
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTrip_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;

            Trip trip = btn.DataContext as Trip;

            _tripOverviewViewModel.DeleteCommand.Execute(trip); 
        }
    }
}
