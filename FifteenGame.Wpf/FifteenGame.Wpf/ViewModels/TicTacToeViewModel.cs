using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StepByStepPacman.WPF.ViewModels
{
    public class TicTacToeViewModel : INotifyPropertyChanged
    {
        public string[] Cells { get; } = new string[9];

        private bool _xTurn = true;
        private bool _gameOver = false;

        public string StatusText
        {
            get
            {
                if (_gameOver) return _statusAfterEnd;
                return _xTurn ? "Ход: X" : "Ход: O";
            }
        }

        private string _statusAfterEnd = "";

        public ICommand CellClickCommand { get; }
        public ICommand ResetCommand { get; }

        public TicTacToeViewModel()
        {
            CellClickCommand = new TicTacToeCommand(p =>
            {
                if (_gameOver) return;
                if (p == null) return;

                int index = Convert.ToInt32(p);
                if (index < 0 || index > 8) return;

                if (!string.IsNullOrEmpty(Cells[index])) return; // клетка занята

                Cells[index] = _xTurn ? "X" : "O";

                // Проверка победы/ничьи
                var winner = GetWinner();
                if (winner != null)
                {
                    _gameOver = true;
                    _statusAfterEnd = $"Победили {winner}!";
                }
                else if (IsDraw())
                {
                    _gameOver = true;
                    _statusAfterEnd = "Ничья!";
                }
                else
                {
                    _xTurn = !_xTurn; // следующий ход
                }

                // Обновляем UI
                OnPropertyChanged(nameof(Cells));
                OnPropertyChanged(nameof(StatusText));
            });

            ResetCommand = new TicTacToeCommand(_ =>
            {
                for (int i = 0; i < 9; i++)
                    Cells[i] = "";

                _xTurn = true;
                _gameOver = false;
                _statusAfterEnd = "";

                OnPropertyChanged(nameof(Cells));
                OnPropertyChanged(nameof(StatusText));
            });
        }

        private string GetWinner()
        {
            int[][] lines =
            {
                new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
                new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
                new[]{0,4,8}, new[]{2,4,6}
            };

            foreach (var l in lines)
            {
                var a = Cells[l[0]];
                if (string.IsNullOrEmpty(a)) continue;

                if (Cells[l[1]] == a && Cells[l[2]] == a)
                    return a; // "X" или "O"
            }

            return null;
        }

        private bool IsDraw()
        {
            for (int i = 0; i < 9; i++)
                if (string.IsNullOrEmpty(Cells[i]))
                    return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Команда с параметром (чтобы CommandParameter работал)
        private sealed class TicTacToeCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public TicTacToeCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
            public void Execute(object parameter) => _execute(parameter);

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
