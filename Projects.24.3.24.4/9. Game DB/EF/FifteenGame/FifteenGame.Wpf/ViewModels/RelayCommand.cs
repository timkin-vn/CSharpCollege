using System;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) { _execute = execute; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }
}