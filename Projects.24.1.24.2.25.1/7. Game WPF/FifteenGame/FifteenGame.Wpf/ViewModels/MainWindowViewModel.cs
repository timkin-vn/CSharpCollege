using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService = new GameService();

        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ICommand CellClickCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new CellViewModel { Row = i / 3, Col = i % 3, Text = "" });
            }

            CellClickCommand = new RelayCommand(param => CellClick((CellViewModel)param));
            NewGameCommand = new RelayCommand(param => StartNewGame());

            StartNewGame();
        }

        private void CellClick(CellViewModel cell)
        {
            if (_gameService.MakeMove(cell.Row, cell.Col))
            {
                UpdateUI();
            }
        }

        private void StartNewGame()
        {
            _gameService.StartNewGame();
            UpdateUI();
        }

        private void UpdateUI()
        {
            for (int i = 0; i < 9; i++)
            {
                int val = _gameService.Model.Board[i / 3, i % 3];
                Cells[i].Text = val == 1 ? "X" : val == 2 ? "O" : "";
            }

            if (_gameService.Model.Winner == 1) StatusText = "Победили Крестики!";
            else if (_gameService.Model.Winner == 2) StatusText = "Победили Нолики!";
            else StatusText = _gameService.Model.CurrentPlayer == 1 ? "Ход: X" : "Ход: O";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}