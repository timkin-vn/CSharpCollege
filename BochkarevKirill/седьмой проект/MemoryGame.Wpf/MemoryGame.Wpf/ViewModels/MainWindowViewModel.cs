using MemoryGame.Business.Models;
using MemoryGame.Business.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MemoryGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();
        private readonly MemoryGameModel _model = new MemoryGameModel();

        public ObservableCollection<CellViewModel> Cells { get; private set; } = new ObservableCollection<CellViewModel>();

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            private set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public RelayCommand NewGameCommand { get; private set; }
        public RelayCommand CellClickCommand { get; private set; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(_ => NewGame());
            CellClickCommand = new RelayCommand(p => OnCellClick(p as CellViewModel));

            NewGame();
        }

        private void NewGame()
        {
            _service.Initialize(_model);
            StatusText = "Найди все пары!";
            FromModel();
        }

        private void OnCellClick(CellViewModel cell)
        {
            if (cell == null) return;

            // Если есть неверная пара — следующий клик закрывает её (это отдельный пошаговый шаг),
            // и в этот же клик мы НЕ открываем новую клетку.
            if (_model.NeedToHideMismatchedPair)
            {
                _service.CommitTurn(_model);
                FromModel();
                return;
            }

            _service.PickCell(_model, cell.Row, cell.Column);
            FromModel();

            if (_model.IsGameOver && _model.IsWin)
                StatusText = "Победа! Все пары найдены.";
        }

        private void FromModel()
        {
            Cells.Clear();
            for (int r = 0; r < MemoryGameModel.RowCount; r++)
                for (int c = 0; c < MemoryGameModel.ColumnCount; c++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _model[r, c],
                        IsRevealed = _model.IsRevealed(r, c),
                        IsMatched = _model.IsMatched(r, c)
                    });
                }

            OnPropertyChanged("Cells");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
