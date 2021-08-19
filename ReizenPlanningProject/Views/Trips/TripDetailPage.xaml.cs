using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.ViewModel.Commands;
using ReizenPlanningProject.ViewModel.Trips;
using ReizenPlanningProject.Views.Itinerary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReizenPlanningProject.Views.Trips
{

    public sealed partial class TripDetailPage : Page
    {

        public TripDetailViewModel _detailVM { get; set; }

        public TripDetailPage()
        {
            this.InitializeComponent();
            _detailVM = new TripDetailViewModel();
            this.DataContext = _detailVM;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (object sender, BackRequestedEventArgs e) => this.NavigateToTripList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _detailVM.Trip = (Trip)e.Parameter;
            _detailVM.Initialize(); 
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

                    var data = itemsLv.ItemFromContainer(itemContainer);
                    itemsLv.SelectedItem = data;
                }
            }
        }

        public void MenuFlyoutTripTask_Opening(object sender, object e)
        {
            MenuFlyout senderAsMenuFlyout = sender as MenuFlyout;

            foreach (object menuFlyoutItem in senderAsMenuFlyout.Items)
            {
                if (menuFlyoutItem.GetType() == typeof(MenuFlyoutItem))
                {
                    // Associate the particular Item with the menu flyout (so the MenuFlyoutItem knows which Item to act upon)
                    ListViewItem itemContainer = senderAsMenuFlyout.Target as ListViewItem;

                    var data = tripTaskLv.ItemFromContainer(itemContainer);
                    tripTaskLv.SelectedItem = data;
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            TripItem ti = (TripItem)itemsLv.SelectedItem;
            _detailVM.DeleteTripItemCommand.Execute(ti);
        }

        private void TripTaskMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            TripTask a = (TripTask)tripTaskLv.SelectedItem;
            _detailVM.DeleteTripTaskCommand.Execute(a);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox c = (ComboBox)sender;
            string action = (string)c.SelectedItem;

            switch (action)
            {

                case "Add category":
                    _detailVM.AddCategoryCommand.Execute(null);
                    break;

                case "Delete category":
                    _detailVM.DeleteCategoryCommand.Execute(null);
                    break;

                case "Add trip item":
                    _detailVM.AddTripItemCommand.Execute(null);
                    break;

                case "Add general item":
                    _detailVM.AddGeneralItemCommand.Execute(null);
                    break;

                default:
                    break;
            }

            c.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ItineraryPage), _detailVM.Trip);
        }

        private void NavigateToTripList()
        {
            Frame.Navigate(typeof(TripsOverviewPage));
        }
    }
}
