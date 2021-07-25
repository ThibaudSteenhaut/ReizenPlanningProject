using Newtonsoft.Json;
using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model.Authentication;
using ReizenPlanningProject.Vault;
using ReizenPlanningProject.ViewModel.Commands;
using ReizenPlanningProject.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ReizenPlanningProject.ViewModel.Login
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly AccountRepository _accountRepository = new AccountRepository();

        public event PropertyChangedEventHandler PropertyChanged;
        
        private bool _showError = false;
        private bool _showSucces = false;

        #endregion

        #region Properties 

        public LoginRequest LoginRequest { get; set; }

        public RelayCommand LoginCommand { get; private set; }
        
        public bool ShowError {

            get { return this._showError; }
            set
            {
                this._showError = value;
                this.RisePropertyChanged(nameof(this.ShowError));
            }
        }

        public bool ShowSucces
        {
            get { return this._showSucces; }
            set
            {
                this._showSucces = value;
                this.RisePropertyChanged(nameof(this.ShowSucces));
            }
        }

        #endregion

        #region Constructor 

        public LoginViewModel()
        {
            LoginRequest = new LoginRequest();
            LoginCommand = new RelayCommand(param => this.Login());
            ShowError = _showError; 
        }

        #endregion

        #region Methods 

        public async void Login() 
        {
            if (!String.IsNullOrEmpty(LoginRequest.Email) && !String.IsNullOrEmpty(LoginRequest.Password))
            {
                string token = await _accountRepository.Login(this.LoginRequest);

                if (String.IsNullOrEmpty(token))
                {
                    ShowError = true;
                }
                else
                {
                    TokenVault.Token = token; 
                    Frame frame = (Frame)Window.Current.Content;
                    frame.Navigate(typeof(MainPage));
                }
            }
        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }
        #endregion
    }
}
