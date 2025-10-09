using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string GameStatus
        {
            get
            {
                if (_model.GameState == GameState.Playing)
                    return $"Очки: {_model.Score} | Ходов осталось: {_model.MovesLeft}";
                else if (_model.GameState == GameState.Won)
                    return $"Победа! Набрано {_model.Score} очков!";
                else
                    return $"Игра окончена. Очки: {_model.Score}";
            }
        }

        public ICommand NewGameCommand { get; }
        public ICommand CellClickCommand { get; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(Initialize);
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);

            Initialize();

            // Временная проверка
            System.Diagnostics.Debug.WriteLine($"Cells count: {Cells.Count}");
            if (Cells.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"First cell GemType: {Cells[0].GemType}");
                System.Diagnostics.Debug.WriteLine($"First cell GemColor: {Cells[0].GemColor}");
            }
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel(_model);
        }

        private void OnCellClick(CellViewModel cellVm)
        {
            if (_model.GameState != GameState.Playing)
                return;

            bool moveMade = _service.SelectGem(_model, cellVm.Row, cellVm.Column);

            if (moveMade)
            {
                FromModel(_model);

                if (_model.GameState != GameState.Playing)
                {
                    string message = _model.GameState == GameState.Won ?
                        "Поздравляем с победой! Начать новую игру?" :
                        "Игра окончена. Попробовать снова?";

                    if (MessageBox.Show(message, "Игра окончена",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Initialize();
                    }
                }
            }
            else
            {
                FromModel(_model);
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        GemType = model[row, column],
                        IsSelected = (row == model.SelectedRow && column == model.SelectedColumn),
                        IsMatched = model.Matched[row, column],
                        ClickCommand = CellClickCommand
                    });
                }
            }

            OnPropertyChanged(nameof(GameStatus));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute) => _execute = execute;

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute) => _execute = execute;

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute((T)parameter);
    }
}