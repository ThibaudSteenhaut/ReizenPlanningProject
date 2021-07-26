using ReizenPlanningProject.ViewModel.Items;
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
    }
}
