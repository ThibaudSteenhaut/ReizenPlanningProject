using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel;
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

namespace ReizenPlanningProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public DetailsPageViewModel _detailsPageViewModel { get; set; }

        public DetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Trip selectedTrip = (Trip)e.Parameter;

            this._detailsPageViewModel = new DetailsPageViewModel(selectedTrip);
            this.DataContext = _detailsPageViewModel;
        }

        private async void AddCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryToTripContentDialog dialog = new AddCategoryToTripContentDialog(_detailsPageViewModel.Categories);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
               // dialog.checkedCategories
            }
        }

    }
}
