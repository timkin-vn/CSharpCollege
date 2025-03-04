using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService = new GameService();
        private readonly GameModel _gameModel = new GameModel();

        public ObservableCollection<int> Cells { get; } = new ObservableCollection<int>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }

        public MainWindowViewModel()
        {
            MoveLeftCommand = new RelayCommand(_ => MakeMove(MoveDirection.Left));
            MoveRightCommand = new RelayCommand(_ => MakeMove(MoveDirection.Right));
            MoveUpCommand = new RelayCommand(_ => MakeMove(MoveDirection.Up));
            MoveDownCommand = new RelayCommand(_ => MakeMove(MoveDirection.Down));

            Initialize();
        }

        public void MakeMove(MoveDirection direction)
        {
            if (_gameService.Move(_gameModel, direction))
            {
                UpdateCells();

                if (_gameService.HasWon(_gameModel))
                {
                    MessageBox.Show("Поздравляем! Вы собрали 2048!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                    Initialize();
                    return;
                }

                if (_gameService.IsGameOver(_gameModel))
                {
                    MessageBox.Show("Игра окончена! Нет доступных ходов.", "Конец игры", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Initialize();
                }
            }
        }

        public void Initialize()
        {
            _gameModel.Reset();
            UpdateCells();
        }

        private void UpdateCells()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    Cells.Add(_gameModel[row, col]);
                }
            }
            OnPropertyChanged(nameof(Cells));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}