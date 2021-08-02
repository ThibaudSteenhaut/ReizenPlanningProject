using ReizenPlanningProject.Model;
using ReizenPlanningProject.Vault;
using ReizenPlanningProject.ViewModel;
using ReizenPlanningProject.Views.Items;
using ReizenPlanningProject.Views.Login;
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

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(TripsOverviewPage));

        }

        private void nvMain_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            var item = (NavigationViewItem)args.SelectedItem;

            switch (item.Name)
            {

                case "TripListItem":
                    contentFrame.Navigate(typeof(TripsOverviewPage));
                    break;

                case "AddTripItem":
                    contentFrame.Navigate(typeof(AddTripPage));
                    break;

                case "GeneralItems":
                    contentFrame.Navigate(typeof(GeneralItemPage));
                    break;

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            switch ((string)args.Parameter)
            {
                case "TripList":
                    nvMain.SelectedItem = TripListItem;
                    break;
            }
        }

        private void LogoutItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TokenVault.Token = "";
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
