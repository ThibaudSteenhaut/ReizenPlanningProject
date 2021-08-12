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


namespace ReizenPlanningProject.Views.Dialogs
{
    public sealed partial class DeleteCategoryDialog : ContentDialog
    {

        public Category SelectedCategory { get; set; }

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public DeleteCategoryDialog()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public DeleteCategoryDialog(ObservableCollection<Category> categories) : this( )
        {
            categories.OrderBy(c => c.Name).ToList().ForEach(c => Categories.Add(c));
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
