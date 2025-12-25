using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Pacman.Business.TicTacToe;

namespace FifteenGame.Wpf.ViewModels
{
    public class TicTacToeViewModel : INotifyPropertyChanged
    {
        private TicTacToeGame _game;

        public string[] Cells => _game.Cells;

        public string StatusText => _game.StatusText;

        public ICommand CellClickCommand { get; }
        public ICommand ResetCommand { get; }

        public TicTacToeViewModel()
        {
            _game = new TicTacToeGame();

            CellClickCommand = new FifteenGame.Wpf.ViewModels.RelayCommand<string>(p =>
            {
                if (p == null) return;
                int index = Convert.ToInt32(p);
                _game.MakeMove(index);
                OnPropertyChanged(nameof(Cells));
                OnPropertyChanged(nameof(StatusText));
            });

            ResetCommand = new FifteenGame.Wpf.ViewModels.RelayCommand(() =>
            {
                _game.ResetGame();
                OnPropertyChanged(nameof(Cells));
                OnPropertyChanged(nameof(StatusText));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
