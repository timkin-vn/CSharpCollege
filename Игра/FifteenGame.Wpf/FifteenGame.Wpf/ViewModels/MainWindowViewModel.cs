using FifteenGame.Services;
using FifteenGame.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FifteenGame.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private ObservableCollection<ObservableCollection<CellViewModel>> _field;

        public MainWindowViewModel()
        {
            _gameService = new GameService(10, 10, 10); // 10x10 поле, 10 мин
            _gameService.GameOver += () => MessageBox.Show("Игра окончена! Вы подорвались на мине.");
            _gameService.GameWon += () => MessageBox.Show("Поздравляю! Ты победил!");
            InitializeField();
        }

        public ObservableCollection<ObservableCollection<CellViewModel>> Field
        {
            get => _field;
            set
            {
                _field = value;
                OnPropertyChanged(nameof(Field));
            }
        }

        private void InitializeField()
        {
            var gameField = _gameService.GameModel.Field;
            Field = new ObservableCollection<ObservableCollection<CellViewModel>>();

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                var row = new ObservableCollection<CellViewModel>();
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    var cell = new CellViewModel(i, j)
                    {
                        OnOpen = (r, c) =>
                        {
                            _gameService.OpenCell(r, c, UpdateField);
                        },
                        OnFlag = (r, c) =>
                        {
                            _gameService.FlagCell(r, c);
                            UpdateField();
                        }
                    };
                    row.Add(cell);
                }
                Field.Add(row);
            }
            UpdateField();
        }

        private void UpdateField()
        {
            var gameField = _gameService.GameModel.Field;
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    Field[i][j].IsOpen = gameField[i, j].IsOpen;
                    Field[i][j].IsFlagged = gameField[i, j].IsFlagged;
                    Field[i][j].IsMine = gameField[i, j].IsMine;
                    Field[i][j].NeighborMines = gameField[i, j].NeighborMines;
                }
            }
            OnPropertyChanged(nameof(Field)); // Уведомляем интерфейс об изменении
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}