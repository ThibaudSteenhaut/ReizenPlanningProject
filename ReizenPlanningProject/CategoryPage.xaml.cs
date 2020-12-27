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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReizenPlanningProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {

        private CategoryOverviewViewModel _categoryViewModel { get; set; }

        public CategoryPage()
        {
            this.InitializeComponent();
            this._categoryViewModel = new CategoryOverviewViewModel(); 
            this.DataContext = _categoryViewModel;
        }

        private void Category_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category cat = (Category)CategoryGrid.SelectedItem;
            if (cat != null) 
            {
                this._categoryViewModel.SelectedCategory = cat;
            }
        }

        private void Category_Item_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListView listView = (ListView)sender;

            Item item = (Item)listView.SelectedItem;

            if (item != null)
            {
                this._categoryViewModel.SelectedItem = item;
            }

        }

        private async void addCategory_Click(object sender, RoutedEventArgs e)
        {
            AddNewCategoryDialog dialog = new AddNewCategoryDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Debug.WriteLine("add new category");
                Debug.WriteLine(dialog.CategoryName);
                _categoryViewModel.AddCategory(dialog.CategoryName);
            }
        }
    }
}
