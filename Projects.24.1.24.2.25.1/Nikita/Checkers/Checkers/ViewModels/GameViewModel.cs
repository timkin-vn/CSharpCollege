using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers.Business.Models;
using Checkers.Models;

namespace Checkers.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private Game _game;
        private int? _selectedRow;
        private int? _selectedCol;

        public ObservableCollection<Cell> BoardCells { get; set; }
        public string CurrentPlayerText => _game.CurrentPlayer == CheckerColor.White ? "White" : "Black";
        public string WinnerText => _game.Winner == CheckerColor.Black ? "Black wins" : _game.Winner == CheckerColor.White ? "White wins" : "";
        public bool IsGameOver => _game.IsGameOver;
        public ICommand CellCheckCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public GameViewModel(Game game)
        {
            _game = game ??  throw new ArgumentNullException(nameof(game));
            BoardCells = new ObservableCollection<Cell>();
            InitializeBoard();
            _game.UpdateValidMoves();
            RefreshBoard();
            CellCheckCommand = new RelayCommand(OnCellClick);
        }

        private void InitializeBoard()
        {
            BoardCells.Clear();
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    var checker = _game.Board.GetChecker(i, j);
                    BoardCells.Add(new Cell
                    {
                        Row = i,
                        Col = j,
                        Checker = checker,
                        IsDark = (i + j) % 2 == 1,
                        IsSelected = false,
                        IsValidMove = false
                    });
                }
            }
        }

        private void RefreshBoard()
        {
            foreach(var cell in BoardCells)
            {
                cell.Checker = _game.Board.GetChecker(cell.Row, cell.Col);
            }

            OnPropertyChanged(nameof(CurrentPlayerText));
            OnPropertyChanged(nameof(WinnerText));
            OnPropertyChanged(nameof(IsGameOver));
        }
        private void OnCellClick(object parameter)
        {

            if(parameter is Cell cell)
            {
                if (_game.IsGameOver) return;

                if (_selectedRow.HasValue && _selectedCol.HasValue)
                {
                    // Попытка сделать ход
                    if (_game.CurrentMoves.Any(m =>
                        m.fromRow == _selectedRow && m.fromCol == _selectedCol &&
                        m.toRow == cell.Row && m.toCol == cell.Col))
                    {
                        _game.MakeMove(_selectedRow.Value, _selectedCol.Value, cell.Row, cell.Col);
                        ClearSelection();
                        RefreshBoard();
                    }
                    else
                    {
                        // Новое выделение
                        SelectCell(cell);
                    }
                }
                else
                {
                    // Выделяем клетку, если на ней есть шашка текущего игрока
                    SelectCell(cell);
                }
            }
        }
        private void SelectCell(Cell cell)
        {
            ClearSelection();

            if (cell.Checker != null && cell.Checker.Color == _game.CurrentPlayer && !_game.IsGameOver)
            {
                _selectedRow = cell.Row;
                _selectedCol = cell.Col;
                cell.IsSelected = true;
                var possibleMoves = (from m in _game.CurrentMoves
                                     where m.fromRow == cell.Row && m.fromCol == cell.Col
                                     select m).ToList();
                var hasMoves = _game.CurrentMoves.Any(m => m.fromRow == cell.Row && m.fromCol == cell.Col);
                // Показываем возможные ходы

                if(hasMoves)
                {
                    foreach (var (fromRow, fromCol, toRow, toCol) in from m in _game.CurrentMoves
                                          where m.fromRow == cell.Row && m.fromCol == cell.Col
                                          select m)
                    {
                        var targetCell = BoardCells.FirstOrDefault(c => c.Row == toRow && c.Col == toCol);
                        if (targetCell != null)
                        {
                            targetCell.IsValidMove = true;
                        }
                    }
                }
            }
        }

        private void ClearSelection()
        {
            foreach(var cell in BoardCells)
            {
                cell.IsSelected = false;
                cell.IsValidMove = false;
            }

            _selectedRow = null;
            _selectedCol = null;
        }

        public void RestartGame()
        {
            _game.Restart();
            ClearSelection();
            _game.UpdateValidMoves();
            InitializeBoard();
            RefreshBoard();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
