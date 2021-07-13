﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReizenPlanningProject.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {

        private readonly Action<object> _execute;

        private readonly Func<bool> _canExecute; 

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
            Debug.WriteLine("delete command wordt gemaakt met null param voor can execute"); 
        }

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute.Invoke(parameter);
      
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}