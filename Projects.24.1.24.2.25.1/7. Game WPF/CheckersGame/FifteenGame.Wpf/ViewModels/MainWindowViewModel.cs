using CheckersGame.Business.Models;
using CheckersGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CheckersGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameService _service = new GameService();
        private GameModel _model;
        private CellViewModel _selectedCell;
        private bool _isFlipped;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();
        public string CurrentPlayerText => $"Ходит: {(_model.CurrentPlayer == Player.Black ? "Чёрные" : "Белые")}";

        public MainWindowViewModel(GameModel model, bool isFlipped = false)
        {
            _model = model;
            _isFlipped = isFlipped;
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

            // В режиме серии взятий разрешаем только подсвеченные цели
            if (_model.IsCaptureInProgress)
            {
                if (_selectedCell != null && clicked.IsValidTarget)
                {
                    int fromRow = _selectedCell.ModelRow;
                    int fromCol = _selectedCell.ModelCol;
                    int toRow = clicked.ModelRow;
                    int toCol = clicked.ModelCol;
                    _service.MakeMove(_model, fromRow, fromCol, toRow, toCol);
                }
                return;
            }

            // Обычный ход
            if (_selectedCell != null && clicked.IsValidTarget)
            {
                int fromRow = _selectedCell.ModelRow;
                int fromCol = _selectedCell.ModelCol;
                int toRow = clicked.ModelRow;
                int toCol = clicked.ModelCol;

                if (_service.MakeMove(_model, fromRow, fromCol, toRow, toCol))
                    _selectedCell = null;
                else
                    ClearSelection();
                return;
            }

            // Выбор своей фигуры
            bool isOwnPiece = (clicked.Piece == Piece.Black || clicked.Piece == Piece.BlackKing) && _model.CurrentPlayer == Player.Black
                           || (clicked.Piece == Piece.White || clicked.Piece == Piece.WhiteKing) && _model.CurrentPlayer == Player.White;
            if (!isOwnPiece)
            {
                ClearSelection();
                return;
            }

            // Проверка обязательности взятия
            bool hasAnyCapture = _service.HasAnyCapture(_model);
            bool clickedHasCapture = _service.CanCapture(_model, clicked.ModelRow, clicked.ModelCol);

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
            _service.FinishCapture(_model);
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
            var moves = _service.GetAvailableMoves(_model, from.ModelRow, from.ModelCol);
            foreach (var (r, c) in moves)
            {
                var target = Cells.FirstOrDefault(cell => cell.ModelRow == r && cell.ModelCol == c);
                if (target != null) target.IsValidTarget = true;
            }
        }
    }
}