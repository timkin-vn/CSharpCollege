using Игра.Business.Models;
using Игра.Business.Services;
using System.Collections.Generic;

namespace Игра.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase 
    {
        private readonly GameModel _gameModel;
        private readonly GameService _gameService;
        
        public List<CellViewModel> Cells { get; set; }

        private bool _isGameWon;
        
        public bool IsGameWon
        {
            get => _isGameWon;
            set => Set(ref _isGameWon, value); 
        }

        public MainWindowViewModel()
        {
            _gameModel = new GameModel();
            _gameService = new GameService(_gameModel);
            
            InitializeCells();
            UpdateAllCellColors();
        }

        private void InitializeCells()
        {
            Cells = new List<CellViewModel>();
            
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    var cellVm = new CellViewModel(r, c);
                    Cells.Add(cellVm);
                }
            }
        }

        public void OnClick(int row, int col)
        {
            if (IsGameWon)
            {
                return;
            }

            _gameService.Toggle(row, col);

            IsGameWon = _gameService.CheckForWin();

            UpdateAllCellColors();
        }

        private void UpdateAllCellColors()
        {
            bool isWin = IsGameWon;

            foreach (var cellVm in Cells)
            {
                bool IsActive = _gameService.GetCellState(cellVm.Row, cellVm.Column);
                cellVm.UpdateColor(IsActive, isWin);
            }
        }
    }
}