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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReizenPlanningProject
{
    public sealed partial class NewTripContentDialog : ContentDialog
    {
       // public Trip trip;
        public NewTripContentDialog()
        {
            this.InitializeComponent();
           // trip = new Trip();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //SAVE
            String destination = this.Destination.Text;
            String start=null;
            String einde=null;
            DateTime startDate = DateTime.Now;
            DateTime endDate=DateTime.Now;

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

            await new Windows.UI.Popups.MessageDialog(destination+" " +startDate.ToString() +" "+ endDate.ToString()).ShowAsync();

           // trip.Destination = destination;
           // trip.DepartureDate = startDate;
          //  trip.ReturnDate = endDate;
            


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //CANCEL
        }
    }
}
