using Newtonsoft.Json;
using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model.Authentication;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel.Login
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly AccountRepository _accountRepository = new AccountRepository();

        public event PropertyChangedEventHandler PropertyChanged;
        
        private bool _showError = false;

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

        #endregion

        #region Constructor 

        public LoginViewModel()
        {
            this.LoginRequest = new LoginRequest(); 
            LoginCommand = new RelayCommand(Login);
            ShowError = false; 
        }

        #endregion

        #region Methods 

        public async void Login(object obj)
        {

            bool isSucces = await _accountRepository.LoginAsync(this.LoginRequest);

            if(!isSucces)
            {

                ShowError = true; 
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
