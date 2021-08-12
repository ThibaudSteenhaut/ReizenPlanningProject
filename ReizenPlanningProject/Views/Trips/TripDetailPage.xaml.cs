using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.ViewModel.Commands;
using ReizenPlanningProject.ViewModel.Trips;
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

                    //(menuFlyoutItem as MenuFlyoutItem).CommandParameter = data;
                    itemsLv.SelectedItem = data;
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            TripItem ti = (TripItem)itemsLv.SelectedItem;
            _detailVM.DeleteTripItemCommand.Execute(ti);
        }

    }
}
