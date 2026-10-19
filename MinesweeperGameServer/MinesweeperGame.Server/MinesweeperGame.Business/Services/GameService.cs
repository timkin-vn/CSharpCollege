using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinesweeperGame.Common.BusinessDtos;
using MinesweeperGame.Common.BusinessModels;
using MinesweeperGame.Common.Contracts;
using MinesweeperGame.Common.Definitions;
using MinesweeperGame.Common.Dtos;

namespace MinesweeperGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly Random _random = new Random();

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<GameResponse> CreateGameAsync(GameDifficulty difficulty)
        {
            try
            {
                var p = difficulty.GetParameters();
                var board = new GameBoard
                {
                    Id = Guid.NewGuid(),
                    Difficulty = difficulty,
                    Rows = p.Rows,
                    Columns = p.Columns,
                    TotalMines = p.Mines,
                    Status = GameStatus.NotStarted
                };

                for (int r = 0; r < p.Rows; r++)
                    for (int c = 0; c < p.Columns; c++)
                        board.Cells.Add(new Cell(r, c));

                board = await _repository.CreateAsync(board);

                return new GameResponse
                {
                    Success = true,
                    Message = "Игра создана",
                    Data = MapToDto(board)
                };
            }
            catch (Exception ex)
            {
                return new GameResponse { Success = false, Message = ex.Message };
            }
        }

        public async Task<GameResponse> RevealCellAsync(Guid gameId, int row, int column)
        {
            try
            {
                var board = await _repository.GetByIdAsync(gameId);
                if (board == null)
                    return new GameResponse { Success = false, Message = "Игра не найдена" };

                if (board.Status == GameStatus.Won || board.Status == GameStatus.Lost)
                    return new GameResponse { Success = false, Message = "Игра уже завершена" };

                var cell = board.GetCell(row, column);
                if (cell == null)
                    return new GameResponse { Success = false, Message = "Клетка не найдена" };

                if (cell.State == CellState.Flagged)
                    return new GameResponse { Success = false, Message = "Клетка помечена флагом" };

                if (cell.State == CellState.Revealed)
                    return new GameResponse { Success = true, Message = "", Data = MapToDto(board) };

                if (board.Status == GameStatus.NotStarted)
                {
                    PlaceMines(board, row, column);
                    CalculateAdjacentMines(board);
                    board.Status = GameStatus.InProgress;
                }

                if (cell.IsMine)
                {
                    cell.State = CellState.Revealed;
                    board.Status = GameStatus.Lost;
                    board.FinishedAt = DateTime.UtcNow;
                    board.ElapsedTime = board.FinishedAt - board.CreatedAt;
                    RevealAllMines(board);
                }
                else
                {
                    RevealCell(board, cell);
                    if (board.AreAllNonMineCellsRevealed())
                    {
                        board.Status = GameStatus.Won;
                        board.FinishedAt = DateTime.UtcNow;
                        board.ElapsedTime = board.FinishedAt - board.CreatedAt;
                    }
                }

                await _repository.UpdateAsync(board);

                return new GameResponse
                {
                    Success = true,
                    Message = board.Status == GameStatus.Lost ? "Вы проиграли!" :
                              board.Status == GameStatus.Won ? "Победа!" : "",
                    Data = MapToDto(board)
                };
            }
            catch (Exception ex)
            {
                return new GameResponse { Success = false, Message = ex.Message };
            }
        }

        public async Task<GameResponse> ToggleFlagAsync(Guid gameId, int row, int column)
        {
            try
            {
                var board = await _repository.GetByIdAsync(gameId);
                if (board == null)
                    return new GameResponse { Success = false, Message = "Игра не найдена" };

                if (board.Status != GameStatus.InProgress && board.Status != GameStatus.NotStarted)
                    return new GameResponse { Success = false, Message = "Игра не активна" };

                var cell = board.GetCell(row, column);
                if (cell == null)
                    return new GameResponse { Success = false, Message = "Клетка не найдена" };

                if (cell.State == CellState.Revealed)
                    return new GameResponse { Success = false, Message = "Клетка уже открыта" };

                if (cell.State == CellState.Flagged)
                {
                    cell.State = CellState.Hidden;
                    board.FlagsPlaced--;
                }
                else
                {
                    cell.State = CellState.Flagged;
                    board.FlagsPlaced++;
                }

                await _repository.UpdateAsync(board);

                return new GameResponse { Success = true, Message = "Флаг изменён", Data = MapToDto(board) };
            }
            catch (Exception ex)
            {
                return new GameResponse { Success = false, Message = ex.Message };
            }
        }

        public async Task<GameResponse> GetGameAsync(Guid gameId)
        {
            try
            {
                var board = await _repository.GetByIdAsync(gameId);
                if (board == null)
                    return new GameResponse { Success = false, Message = "Игра не найдена" };
                return new GameResponse { Success = true, Data = MapToDto(board) };
            }
            catch (Exception ex)
            {
                return new GameResponse { Success = false, Message = ex.Message };
            }
        }

        private void PlaceMines(GameBoard board, int safeRow, int safeCol)
        {
            var safeCells = new HashSet<(int, int)> { (safeRow, safeCol) };
            foreach (var adj in board.GetAdjacentCells(safeRow, safeCol))
                safeCells.Add((adj.Row, adj.Column));

            var available = board.Cells.Where(c => !safeCells.Contains((c.Row, c.Column))).ToList();
            var mines = available.OrderBy(_ => _random.Next()).Take(board.TotalMines).ToList();
            foreach (var cell in mines) cell.IsMine = true;
        }

        private void CalculateAdjacentMines(GameBoard board)
        {
            foreach (var cell in board.Cells)
            {
                if (cell.IsMine) continue;
                cell.AdjacentMinesCount = board.GetAdjacentCells(cell.Row, cell.Column).Count(c => c.IsMine);
            }
        }

        private void RevealCell(GameBoard board, Cell cell)
        {
            if (cell.State == CellState.Revealed) return;
            cell.State = CellState.Revealed;
            if (cell.AdjacentMinesCount == 0 && !cell.IsMine)
                foreach (var adj in board.GetAdjacentCells(cell.Row, cell.Column))
                    if (adj.State != CellState.Revealed && adj.State != CellState.Flagged)
                        RevealCell(board, adj);
        }

        private void RevealAllMines(GameBoard board)
        {
            foreach (var cell in board.Cells.Where(c => c.IsMine))
                if (cell.State != CellState.Flagged) cell.State = CellState.Revealed;
        }

        private GameBoardDto MapToDto(GameBoard board)
        {
            var showMines = board.Status == GameStatus.Lost || board.Status == GameStatus.Won;
            return new GameBoardDto
            {
                Game = new GameDto
                {
                    Id = board.Id,
                    Difficulty = board.Difficulty,
                    Status = board.Status,
                    Rows = board.Rows,
                    Columns = board.Columns,
                    TotalMines = board.TotalMines,
                    FlagsPlaced = board.FlagsPlaced,
                    CreatedAt = board.CreatedAt,
                    FinishedAt = board.FinishedAt,
                    ElapsedSeconds = board.ElapsedTime?.TotalSeconds
                },
                Cells = board.Cells.Select(c => new CellDto
                {
                    Row = c.Row,
                    Column = c.Column,
                    IsMine = showMines ? c.IsMine : false,
                    AdjacentMinesCount = c.State == CellState.Revealed ? c.AdjacentMinesCount : 0,
                    State = c.State
                }).ToList()
            };
        }
    }
}
