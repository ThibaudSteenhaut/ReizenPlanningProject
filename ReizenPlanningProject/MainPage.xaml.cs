using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReizenPlanningProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Trip t;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new MainPageViewModel();
            //t = new Trip() { Destination = "Peru", DepartureDate = new DateTime(2022, 12, 30), ReturnDate = new DateTime(2023, 1, 10) };

        }

        private async void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new NewTripContentDialog();
            await dialog.ShowAsync();

            //t = dialog.trip;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            ContentDialogResult result = await test.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                Debug.WriteLine(this.Destination.Text);
            }

            String start = null;
            String einde = null;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            if (this.StartDate.SelectedDate != null)
            {
                start = this.StartDate.SelectedDate.ToString();
                startDate = Convert.ToDateTime(start);
            }
            if (this.StartDate.SelectedDate != null)
            {
                einde = this.EndDate.SelectedDate.ToString();
                endDate = Convert.ToDateTime(einde);
            }

            t = new Trip { Destination = this.Destination.Text, DepartureDate = startDate, ReturnDate = endDate };




        }


    }
}
