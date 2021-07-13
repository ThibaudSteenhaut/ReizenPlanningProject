using ReizenPlanningProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AddCategoryToTripContentDialog : ContentDialog
    {
        public List<Category> checkedCategories;
        public AddCategoryToTripContentDialog(ObservableCollection<Category> categories)
        {
            this.InitializeComponent();
            checkedCategories = new List<Category>();
            listView.ItemsSource = categories;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            foreach (var item in listView.SelectedItems)
                checkedCategories.Add((Category)item);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
