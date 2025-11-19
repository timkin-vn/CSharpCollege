using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    // High-level service that manages two fields and turns
    public class GameService
    {
        public GameField PlayerField { get; private set; }
        public GameField EnemyField { get; private set; }

        public bool IsPlayerTurn { get; private set; } = true;
        private Random rng = new Random();

        // pool of all enemy candidate cells for AI random shots
        private HashSet<(int X, int Y)> enemyTargetCandidates = new HashSet<(int X, int Y)>();

        // Standard fleet lengths: Carrier(5), Battleship(4), Cruiser(3), Submarine(3), Destroyer(2)
        public static readonly int[] DefaultFleet = new[] { 5, 4, 3, 3, 2 };

        public GameService(int width = GameField.DefaultSize, int height = GameField.DefaultSize)
        {
            PlayerField = new GameField(width, height);
            EnemyField = new GameField(width, height);
            ResetEnemyTargets();
        }

        private void ResetEnemyTargets()
        {
            enemyTargetCandidates.Clear();
            for (int x = 0; x < EnemyField.Width; x++)
                for (int y = 0; y < EnemyField.Height; y++)
                    enemyTargetCandidates.Add((x, y));
        }

        public void StartNewGame(bool autoPlacePlayer = false, IEnumerable<int> fleet = null)
        {
            fleet ??= DefaultFleet;
            PlayerField.Clear();
            EnemyField.Clear();
            ResetEnemyTargets();
            IsPlayerTurn = true;

            if (autoPlacePlayer)
                PlayerField.AutoPlaceFleet(fleet);

            EnemyField.AutoPlaceFleet(fleet);
        }

        // Player places ship manually. Returns true if placed.
        public bool PlayerPlaceShip(int startX, int startY, Orientation orientation, int length)
        {
            return PlayerField.PlaceShip(startX, startY, orientation, length);
        }

        // Player fires at enemy
        public ShotResult PlayerFire(int x, int y)
        {
            if (!IsPlayerTurn) throw new InvalidOperationException("Not player's turn");
            var result = EnemyField.Shoot(x, y);
            if (result == ShotResult.Miss)
                IsPlayerTurn = false; // switch to enemy
            // If Hit or Sunk, player continues
            return result;
        }

        // Enemy (AI) makes a move: simple random selection among unseen cells
        public (int X, int Y, ShotResult Result) EnemyMove()
        {
            if (IsPlayerTurn) throw new InvalidOperationException("Not enemy's turn");

            if (!enemyTargetCandidates.Any())
                ResetEnemyTargets();

            // Simple AI: pick random from candidates
            var list = enemyTargetCandidates.ToList();
            var pick = list[rng.Next(list.Count)];
            enemyTargetCandidates.Remove(pick);

            var result = PlayerField.Shoot(pick.X, pick.Y);

            // If miss - switch back to player
            if (result == ShotResult.Miss)
                IsPlayerTurn = true;
            // If hit or sunk - enemy continues (we keep IsPlayerTurn = false)

            return (pick.X, pick.Y, result);
        }

        public bool IsPlayerWinner() => EnemyField.AllShipsSunk();
        public bool IsEnemyWinner() => PlayerField.AllShipsSunk();

        // Helpers for UI: get masked enemy view (so player doesn't see ships)
        public Cell[,] GetEnemyViewForPlayer() => EnemyField.GetMaskedGridForOpponent();

        // Helper: get player's field (with ships visible)
        public Cell[,] GetPlayerView() => PlayerField.GetFullGridCopy();

        // Convenience: render both fields to strings for console testing
        public string DebugRenderBoth(bool showEnemyShips = false)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Player field:");
            sb.AppendLine(PlayerField.RenderToString(showShips: true));
            sb.AppendLine("Enemy field (player view):");
            sb.AppendLine(EnemyField.RenderToString(showShips: showEnemyShips));
            return sb.ToString();
        }
    }
}
