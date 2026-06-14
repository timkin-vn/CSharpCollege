using CheckersGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersGame.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            model.EndCaptureSequence();
            for (int r = 0; r < GameModel.BoardSize; r++)
                for (int c = 0; c < GameModel.BoardSize; c++)
                    model[r, c] = Piece.None;

            // Чёрные сверху на чёрных клетках
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < GameModel.BoardSize; c++)
                    if ((r + c) % 2 == 0)
                        model[r, c] = Piece.Black;

            // Белые снизу на чёрных клетках
            for (int r = 5; r < 8; r++)
                for (int c = 0; c < GameModel.BoardSize; c++)
                    if ((r + c) % 2 == 0)
                        model[r, c] = Piece.White;

            model.CurrentPlayer = Player.White;
        }

        public bool HasAnyCapture(GameModel model)
        {
            for (int r = 0; r < GameModel.BoardSize; r++)
                for (int c = 0; c < GameModel.BoardSize; c++)
                {
                    var piece = model[r, c];
                    if (piece == Piece.None) continue;
                    bool isBlack = piece == Piece.Black || piece == Piece.BlackKing;
                    if ((model.CurrentPlayer == Player.Black && !isBlack) ||
                        (model.CurrentPlayer == Player.White && isBlack)) continue;

                    if (CanCapture(model, r, c)) return true;
                }
            return false;
        }

        public bool CanCapture(GameModel model, int fromRow, int fromCol)
        {
            var moves = GetAvailableMoves(model, fromRow, fromCol);
            return moves.Any(m => IsCaptureMove(fromRow, fromCol, m.row, m.col, model));
        }

        // Проверяет, является ли ход взятием (без учёта доступности из списка)
        private bool IsCaptureMove(int fromRow, int fromCol, int toRow, int toCol, GameModel model)
        {
            int dr = Math.Sign(toRow - fromRow);
            int dc = Math.Sign(toCol - fromCol);
            if (dr == 0 || dc == 0) return false; // не диагональ

            Piece piece = model[fromRow, fromCol];
            if (piece == Piece.None) return false;
            bool isBlack = piece == Piece.Black || piece == Piece.BlackKing;
            bool isKing = piece == Piece.BlackKing || piece == Piece.WhiteKing;

            int r = fromRow + dr, c = fromCol + dc;
            bool foundOpponent = false;

            while (r != toRow || c != toCol)
            {
                if (r < 0 || r >= GameModel.BoardSize || c < 0 || c >= GameModel.BoardSize)
                    return false;

                if (model[r, c] != Piece.None)
                {
                    bool isOpponent = isBlack ? (model[r, c] == Piece.White || model[r, c] == Piece.WhiteKing)
                                              : (model[r, c] == Piece.Black || model[r, c] == Piece.BlackKing);
                    if (!isOpponent) return false;   // встретили свою фигуру — нельзя бить
                    if (foundOpponent) return false; // уже нашли противника, второй раз — ошибка
                    foundOpponent = true;
                }
                r += dr;
                c += dc;
            }

            // Если мы вышли из цикла, проверим, что клетка назначения пуста
            if (model[toRow, toCol] != Piece.None) return false;

            if (isKing)
                return foundOpponent; // для дамки обязателен ровно один противник на пути
            else
            {
                // Простая шашка: расстояние ровно 2 и противник на средней клетке
                int dist = Math.Abs(toRow - fromRow);
                if (dist != 2) return false;
                return foundOpponent;
            }
        }

        public List<(int row, int col)> GetAvailableMoves(GameModel model, int fromRow, int fromCol)
        {
            // Если идёт серия взятий, ходить может только бьющая фигура
            if (model.IsCaptureInProgress &&
                (fromRow != model.CaptureRow || fromCol != model.CaptureCol))
                return new List<(int, int)>();

            var piece = model[fromRow, fromCol];
            if (piece == Piece.None) return new List<(int, int)>();

            bool isBlack = piece == Piece.Black || piece == Piece.BlackKing;
            bool isKing = piece == Piece.BlackKing || piece == Piece.WhiteKing;
            if ((model.CurrentPlayer == Player.Black && !isBlack) ||
                (model.CurrentPlayer == Player.White && isBlack))
                return new List<(int, int)>();

            var moves = new List<(int, int)>();
            var captures = new List<(int, int)>();

            int forward = isBlack ? 1 : -1;
            int[] drs = { -1, -1, 1, 1 };
            int[] dcs = { -1, 1, -1, 1 };

            for (int dir = 0; dir < 4; dir++)
            {
                int dr = drs[dir];
                int dc = dcs[dir];

                if (!isKing)
                {
                    // Простые шашки
                    int nr = fromRow + dr;
                    int nc = fromCol + dc;
                    if (nr < 0 || nr >= GameModel.BoardSize || nc < 0 || nc >= GameModel.BoardSize)
                        continue;

                    if (model[nr, nc] == Piece.None)
                    {
                        if (dr == forward)
                            moves.Add((nr, nc));
                    }
                    else
                    {
                        bool isOwn = isBlack ? (model[nr, nc] == Piece.Black || model[nr, nc] == Piece.BlackKing)
                                              : (model[nr, nc] == Piece.White || model[nr, nc] == Piece.WhiteKing);
                        if (isOwn) continue;
                        if (model.CapturedThisTurn.Contains((nr, nc))) continue;

                        int landR = nr + dr;
                        int landC = nc + dc;
                        if (landR >= 0 && landR < GameModel.BoardSize && landC >= 0 && landC < GameModel.BoardSize &&
                            model[landR, landC] == Piece.None)
                        {
                            captures.Add((landR, landC));
                        }
                    }
                }
                else
                {
                    // Дамки
                    int r = fromRow + dr;
                    int c = fromCol + dc;
                    bool opponentFound = false;
                    int oppR = -1, oppC = -1;

                    while (r >= 0 && r < GameModel.BoardSize && c >= 0 && c < GameModel.BoardSize)
                    {
                        if (model[r, c] == Piece.None)
                        {
                            if (!opponentFound)
                                moves.Add((r, c));
                            else
                                captures.Add((r, c)); // после противника все пустые клетки — взятия
                        }
                        else
                        {
                            if (!opponentFound)
                            {
                                bool isOwn = isBlack ? (model[r, c] == Piece.Black || model[r, c] == Piece.BlackKing)
                                                      : (model[r, c] == Piece.White || model[r, c] == Piece.WhiteKing);
                                if (isOwn) break;
                                if (model.CapturedThisTurn.Contains((r, c))) break;

                                opponentFound = true;
                                oppR = r;
                                oppC = c;
                            }
                            else break; // встретили вторую фигуру – дальше нельзя
                        }
                        r += dr;
                        c += dc;
                    }
                }
            }

            // Правило обязательного взятия: если есть взятия, возвращаем только их
            if (captures.Count > 0)
                return captures;

            // Во время серии взятий простые ходы запрещены
            if (model.IsCaptureInProgress)
                return new List<(int, int)>();

            return moves;
        }

        public bool MakeMove(GameModel model, int fromRow, int fromCol, int toRow, int toCol)
        {
            var available = GetAvailableMoves(model, fromRow, fromCol);
            if (!available.Contains((toRow, toCol)))
                return false;

            var piece = model[fromRow, fromCol];
            int dr = Math.Sign(toRow - fromRow);
            int dc = Math.Sign(toCol - fromCol);

            bool isCapture = IsCaptureMove(fromRow, fromCol, toRow, toCol, model);
            if (isCapture)
            {
                // Удаляем побитую шашку (ищем противника по пути)
                int midR = fromRow + dr;
                int midC = fromCol + dc;
                while (midR != toRow || midC != toCol)
                {
                    if (model[midR, midC] != Piece.None)
                    {
                        model[midR, midC] = Piece.None;
                        model.CapturedThisTurn.Add((midR, midC));
                        break;
                    }
                    midR += dr;
                    midC += dc;
                }
            }

            model[toRow, toCol] = piece;
            model[fromRow, fromCol] = Piece.None;

            // Превращение в дамку
            if (piece == Piece.Black && toRow == GameModel.BoardSize - 1)
                model[toRow, toCol] = Piece.BlackKing;
            else if (piece == Piece.White && toRow == 0)
                model[toRow, toCol] = Piece.WhiteKing;

            if (isCapture)
            {
                // Проверяем, можно ли бить дальше той же фигурой
                var subsequentCaptures = GetAvailableMoves(model, toRow, toCol)
                    .Where(m => IsCaptureMove(toRow, toCol, m.row, m.col, model)).ToList();
                if (subsequentCaptures.Any())
                    model.StartCaptureSequence(toRow, toCol);
                else
                {
                    model.EndCaptureSequence();
                    model.SwitchPlayer();
                }
            }
            else
            {
                model.EndCaptureSequence();
                model.SwitchPlayer();
            }

            model.RaiseModelChanged();
            return true;
        }

        public void FinishCapture(GameModel model)
        {
            if (model.IsCaptureInProgress)
            {
                model.EndCaptureSequence();
                model.SwitchPlayer();
                model.RaiseModelChanged();
            }
        }

        public bool IsGameOver(GameModel model) => model.IsGameOver();
    }
}