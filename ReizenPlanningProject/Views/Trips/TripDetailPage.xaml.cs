using ReizenPlanningProject.Model;
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
    }
}
