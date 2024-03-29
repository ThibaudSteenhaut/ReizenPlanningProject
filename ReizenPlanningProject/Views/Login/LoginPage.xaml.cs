﻿using ReizenPlanningProject.ViewModel;
using ReizenPlanningProject.ViewModel.Login;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ReizenPlanningProject.Views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel _loginVM { get; set; }

        public LoginPage()
        {
            this.InitializeComponent();
            this._loginVM = new LoginViewModel();
            this.DataContext = _loginVM;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is bool)
            {
                bool showSucces = (bool)e.Parameter;
                _loginVM.ShowSucces = showSucces; 
            }

            base.OnNavigatedTo(e);
        }
    }
}
