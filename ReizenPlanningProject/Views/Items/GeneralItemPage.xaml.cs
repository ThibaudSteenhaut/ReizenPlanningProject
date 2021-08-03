using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Items;
using ReizenPlanningProject.Views.Dialogs;
using System;
using System.Collections.Generic;
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


namespace ReizenPlanningProject.Views.Items
{
    public sealed partial class GeneralItemPage : Page
    {
        private GeneralItemsViewModel _itemsVM { get; set; }

        public GeneralItemPage()
        {

            this.InitializeComponent();
            _itemsVM = new GeneralItemsViewModel();

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

            Item i = (Item)itemsLv.SelectedItem;
            Debug.WriteLine(i);
            _itemsVM.DeleteItemCommand.Execute(i);
        }
    }
}
