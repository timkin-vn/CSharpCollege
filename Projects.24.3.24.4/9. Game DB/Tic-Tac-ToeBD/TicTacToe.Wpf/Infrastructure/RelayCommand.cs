using System;
using System.Windows.Input;

namespace TicTacToe.Wpf.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        // Конструктор
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Проверка: можно ли выполнить команду
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Выполнение команды
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // Событие, которое сообщает WPF, изменилось ли состояние кнопки (доступна/недоступна)
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}