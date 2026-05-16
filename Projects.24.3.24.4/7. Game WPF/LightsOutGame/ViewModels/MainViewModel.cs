using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LightsOutGame.Models;

namespace LightsOutGame.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly GameModel _game;

        public ObservableCollection<CellViewModel> Cells { get; }

        public ICommand CellClickCommand { get; }
        public ICommand NewGameCommand { get; }

        public MainViewModel()
        {
            _game = new GameModel(5);
            Cells = new ObservableCollection<CellViewModel>();

            CellClickCommand = new RelayCommand(OnCellClicked);
            NewGameCommand = new RelayCommand(_ => NewGame());

            CreateCells();
        }

        private void CreateCells()
        {
            Cells.Clear();

            for (int i = 0; i < _game.Size; i++)
                for (int j = 0; j < _game.Size; j++)
                    Cells.Add(new CellViewModel(_game.Cells[i, j], i, j, CellClickCommand));
        }

        private void OnCellClicked(object parameter)
        {
            if (parameter is CellViewModel cell)
            {
                _game.MakeMove(cell.Row, cell.Column);
                RefreshCells();

                if (_game.IsWin())
                    MessageBox.Show("Поздравляем! Вы выключили весь свет!", "Победа");
            }
        }

        private void RefreshCells()
        {
            foreach (var cell in Cells)
                cell.Refresh();
        }

        private void NewGame()
        {
            _game.Initialize();
            RefreshCells();
        }
    }
}
