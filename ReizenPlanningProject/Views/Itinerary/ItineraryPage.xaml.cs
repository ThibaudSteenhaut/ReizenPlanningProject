using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Itinerary;
using ReizenPlanningProject.Views.Trips;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ReizenPlanningProject.Views.Itinerary
{

    public sealed partial class ItineraryPage : Page
    {

        public ItineraryViewModel ItineraryVM { get; set; }

        public ItineraryPage()
        {
            this.InitializeComponent();
            this.ItineraryVM = new ItineraryViewModel();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (object sender, BackRequestedEventArgs e) => this.NavigateToDetailPage(ItineraryVM.Trip);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ItineraryVM.Trip = (Trip)e.Parameter;
            ItineraryVM.Initialize();
        }

        public void MenuFlyout_Opening(object sender, object e)
        {
            MenuFlyout senderAsMenuFlyout = sender as MenuFlyout;

            foreach (object menuFlyoutItem in senderAsMenuFlyout.Items)
            {
                if (menuFlyoutItem.GetType() == typeof(MenuFlyoutItem))
                {
                    // Associate the particular Item with the menu flyout (so the MenuFlyoutItem knows which Item to act upon)
                    ListViewItem itemContainer = senderAsMenuFlyout.Target as ListViewItem;

                    var data = itineraryLv.ItemFromContainer(itemContainer);

                    itineraryLv.SelectedItem = data;

                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ItineraryItem ii = (ItineraryItem)itineraryLv.SelectedItem;
            ItineraryVM.DeleteItineraryItemCommand.Execute(ii);
        }

        private void NavigateToDetailPage(Trip trip)
        {
            Frame.Navigate(typeof(TripDetailPage), trip);
        }
    }
}
