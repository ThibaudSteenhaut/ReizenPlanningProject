using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public sealed partial class AddGeneralItemDialog : ContentDialog
    {

        private readonly IItemRepository _itemRepository = new ItemRepository();
        private List<Item> _items { get; set; }

        public ObservableCollection<Category> GeneralCategories { get; set; }
        public Category SelectedCategory { get; set; }

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        public Item SelectedItem { get; set; }
        public int Amount { get; set; } = 1;

        public AddGeneralItemDialog()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GeneralCategories = _itemRepository.GetCategories();
            _items = _itemRepository.GetItems().ToList(); 
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Category c = (Category)comboBox.SelectedItem;

            Debug.WriteLine(c); 

            Items.Clear();

            foreach (Item item in _items.Where(i => i.Category.Id == c.Id))
            {
                Items.Add(item);
            }
        }
    }
}
