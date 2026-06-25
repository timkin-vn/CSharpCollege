using CheckersGame.Business.Models;
using CheckersGame.Business.Services;
using CheckersGame.Business.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CheckersGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CheckersGame.Business.Contracts.ICheckersGameService _gameService;
        private readonly GameService _coreService = new GameService();
        private GameModel _model;
        private CellViewModel _selectedCell;
        private bool _isFlipped;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();
        public string CurrentPlayerText => $"Ходит: {(_model.CurrentPlayer == Player.Black ? "Чёрные" : "Белые")}";

        public MainWindowViewModel(GameModel model, bool isFlipped, CheckersGame.Business.Contracts.ICheckersGameService gameService)
        {
            _model = model;
            _isFlipped = isFlipped;
            _gameService = gameService;
            _model.ModelChanged += OnModelChanged;
            LoadCells();
        }

        private void OnModelChanged()
        {
            if (System.Windows.Application.Current != null)
            {
                var dispatcher = System.Windows.Application.Current.Dispatcher;
                if (dispatcher.CheckAccess())
                    LoadCells();
                else
                    dispatcher.Invoke(() => LoadCells());
            }
        }

        private void LoadCells()
        {
            Cells.Clear();
            for (int modelRow = 0; modelRow < GameModel.BoardSize; modelRow++)
            {
                for (int modelCol = 0; modelCol < GameModel.BoardSize; modelCol++)
                {
                    int viewRow = _isFlipped ? (GameModel.BoardSize - 1 - modelRow) : modelRow;
                    int viewCol = _isFlipped ? (GameModel.BoardSize - 1 - modelCol) : modelCol;

                    var vm = new CellViewModel
                    {
                        Row = viewRow,
                        Column = viewCol,
                        ModelRow = modelRow,
                        ModelCol = modelCol,
                        Piece = _model[modelRow, modelCol],
                        IsSelected = false,
                        IsValidTarget = false
                    };
                    Cells.Add(vm);
                }
            }

            if (_model.IsCaptureInProgress)
            {
                var capturer = Cells.FirstOrDefault(c => c.ModelRow == _model.CaptureRow && c.ModelCol == _model.CaptureCol);
                if (capturer != null)
                {
                    capturer.IsSelected = true;
                    _selectedCell = capturer;
                    HighlightMoves(capturer);
                }
            }
            else
            {
                _selectedCell = null;
            }

            OnPropertyChanged(nameof(CurrentPlayerText));
        }

        public void CellClicked(CellViewModel clicked)
        {
            if (clicked == null) return;

            if (_model.IsCaptureInProgress)
            {
                if (_selectedCell != null && clicked.IsValidTarget)
                {
                    int fromRow = _selectedCell.ModelRow;
                    int fromCol = _selectedCell.ModelCol;
                    int toRow = clicked.ModelRow;
                    int toCol = clicked.ModelCol;
                    try
                    {
                        _gameService.MakeMove(_model.Id, fromRow, fromCol, toRow, toCol);
                        _model = _gameService.LoadGame(_model.Id);
                        LoadCells();
                    }
                    catch (Exception)
                    {
                        // Ход не удался - сбрасываем выделение
                        ClearSelection();
                    }
                }
                return;
            }

            if (_selectedCell != null && clicked.IsValidTarget)
            {
                int fromRow = _selectedCell.ModelRow;
                int fromCol = _selectedCell.ModelCol;
                int toRow = clicked.ModelRow;
                int toCol = clicked.ModelCol;

                try
                {
                    _gameService.MakeMove(_model.Id, fromRow, fromCol, toRow, toCol);
                    _model = _gameService.LoadGame(_model.Id);
                    _selectedCell = null;
                    LoadCells();
                }
                catch (Exception)
                {
                    ClearSelection();
                }
                return;
            }

            // Выбор своей шашки
            bool isOwnPiece = (clicked.Piece == Piece.Black || clicked.Piece == Piece.BlackKing) && _model.CurrentPlayer == Player.Black
                           || (clicked.Piece == Piece.White || clicked.Piece == Piece.WhiteKing) && _model.CurrentPlayer == Player.White;
            if (!isOwnPiece)
            {
                ClearSelection();
                return;
            }

            bool hasAnyCapture = _coreService.HasAnyCapture(_model);
            bool clickedHasCapture = _coreService.CanCapture(_model, clicked.ModelRow, clicked.ModelCol);

            if (hasAnyCapture && !clickedHasCapture)
            {
                ClearSelection();
                return;
            }

            if (_selectedCell == clicked)
            {
                ClearSelection();
                return;
            }

            ClearSelection();
            _selectedCell = clicked;
            _selectedCell.IsSelected = true;
            HighlightMoves(clicked);
        }

        public void FinishCapture()
        {
            _gameService.FinishCapture(_model.Id);
            _model = _gameService.LoadGame(_model.Id);
            LoadCells();
        }

        private void ClearSelection()
        {
            if (_selectedCell != null)
            {
                _selectedCell.IsSelected = false;
                _selectedCell = null;
            }
            foreach (var cell in Cells)
                cell.IsValidTarget = false;
        }

        private void HighlightMoves(CellViewModel from)
        {
            var moves = _coreService.GetAvailableMoves(_model, from.ModelRow, from.ModelCol);
            foreach (var (r, c) in moves)
            {
                var target = Cells.FirstOrDefault(cell => cell.ModelRow == r && cell.ModelCol == c);
                if (target != null) target.IsValidTarget = true;
            }
        }
    }
}