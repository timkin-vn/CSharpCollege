using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private string _statusText = "Ваш ход";
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        private int _playerScore;
        public int PlayerScore
        {
            get => _playerScore;
            set { _playerScore = value; OnPropertyChanged(nameof(PlayerScore)); }
        }

        private int _computerScore;
        public int ComputerScore
        {
            get => _computerScore;
            set { _computerScore = value; OnPropertyChanged(nameof(ComputerScore)); }
        }

        public ICommand NewGameCommand { get; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(Initialize);
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            Cells.Clear();

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    int localRow = r;
                    int localCol = c;

                    var cell = new CellViewModel
                    {
                        Row = localRow,
                        Column = localCol,
                        ClickCommand = new RelayCommand(() => PlayerMove(localRow, localCol))
                    };
                    Cells.Add(cell);
                }
            }
            UpdateView();
        }

        private void PlayerMove(int r, int c)
        {
            if (_model.Result != GameResult.None) return;

            if (_service.MakeMove(_model, r, c, CellState.X))
            {
                UpdateView();

                if (_model.Result == GameResult.None)
                {
                    System.Threading.Tasks.Task.Delay(300).ContinueWith(_ =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _service.MakeComputerMove(_model);
                            UpdateView();
                        });
                    });
                }
            }
        }

        private void UpdateView()
        {
            foreach (var cell in Cells)
            {
                var state = _model[cell.Row, cell.Column];
                cell.Text = state == CellState.Empty ? "" : state.ToString();
                cell.IsWinningCell = _model.WinningCells.Contains((cell.Row, cell.Column));
            }

            if (_model.Result == GameResult.PlayerWin)
            {
                StatusText = "Вы победили!";
                PlayerScore++;
            }
            else if (_model.Result == GameResult.ComputerWin)
            {
                StatusText = "Компьютер победил!";
                ComputerScore++;
            }
            else if (_model.Result == GameResult.Draw)
            {
                StatusText = "Ничья!";
            }
            else
            {
                StatusText = "Ваш ход";
            }
        }
    }
}