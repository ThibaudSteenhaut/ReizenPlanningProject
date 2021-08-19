using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model.Authentication;
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
    public class RegisterViewModel : INotifyPropertyChanged
    {

        #region Fields 

        private readonly AccountRepository _accountRepo = new AccountRepository();
        public string _errorMessage = "";
        private bool _showError = false;

        #endregion

        #region Properties 

        public RegisterRequest Request { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand RegisterCommand { get; private set; }

        public string ErrorMessage
        {

            get { return this._errorMessage; }
            set
            {
                this._errorMessage = value;
                this.RisePropertyChanged(nameof(this.ErrorMessage));
            }
        }

        public bool ShowError
        {

            get { return this._showError; }
            set
            {
                this._showError = value;
                this.RisePropertyChanged(nameof(this.ShowError));
            }
        }

        #endregion

        #region Constructor

        public RegisterViewModel()
        {
            this.Request = new RegisterRequest();
            RegisterCommand = new RelayCommand(param => this.Register());
            ShowError = _showError;
            ErrorMessage = _errorMessage;
        }

        #endregion

        #region Methods

        private async void Register()
        {

            if (!HasEmptyField())
            {
                if (Request.Password.Equals(Request.PasswordConfirmation))
                {
                    if (Request.Password.Length >= 10)
                    {
                        bool nameAvailable = await _accountRepo.CheckAvailableUserName(Request.Email);

                        if (nameAvailable)
                        {
                            bool succes = await _accountRepo.Register(Request);

                            if (succes)
                            {
                                Frame frame = (Frame)Window.Current.Content;
                                frame.Navigate(typeof(LoginPage), true);
                            }
                            else
                            {
                                DisplayError("Something went wrong, try again later.");
                            }
                        }
                        else
                        {
                            DisplayError("This e-mail is already in use.");
                        }
                    }
                    else
                    {
                        DisplayError("Password must be at least 10 characters.");
                    }
                }
                else
                {
                    DisplayError("The passwords do not match.");
                }
            }

            else
            {
                DisplayError("Please fill out all fields.");
            }
        }

        private void DisplayError(string message)
        {
            ErrorMessage = message;
            ShowError = true;
        }

        private bool HasEmptyField()
        {
            return String.IsNullOrEmpty(Request.Email) ||
                    String.IsNullOrEmpty(Request.Firstname) ||
                    String.IsNullOrEmpty(Request.Lastname) ||
                    String.IsNullOrEmpty(Request.Password) ||
                    String.IsNullOrEmpty(Request.PasswordConfirmation);
        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }

        #endregion
    }
}
