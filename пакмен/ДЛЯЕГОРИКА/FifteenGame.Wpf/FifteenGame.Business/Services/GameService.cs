using StepByStepPacman.Business.Models;
using System;

namespace StepByStepPacman.Business.Services
{
    public class GameService
    {
        public GameState State { get; private set; }
        private int _ghostMoveCounter = 0;
        private const int GHOST_MOVE_DELAY = 8; // Призраки двигаются каждые 8 обновлений

        public GameService()
        {
            State = new GameState();
        }

        public void Update()
        {
            if (!State.GameRunning) return;

            // Двигаем пакмана
            MovePlayer(State.Player.Direction);

            // Замедляем движение призраков
            _ghostMoveCounter++;
            if (_ghostMoveCounter >= GHOST_MOVE_DELAY)
            {
                MoveGhosts();
                _ghostMoveCounter = 0;
            }

            CheckGhostCollisions();
            CheckGameEnd();
        }

        public void MovePlayer(Direction direction)
        {
            if (!State.GameRunning) return;

            int oldX = State.Player.X;
            int oldY = State.Player.Y;
            bool moved = false;

            switch (direction)
            {
                case Direction.Left:
                    moved = State.Player.TryMove(-1, 0, State.GameBoard);
                    break;
                case Direction.Right:
                    moved = State.Player.TryMove(1, 0, State.GameBoard);
                    break;
                case Direction.Up:
                    moved = State.Player.TryMove(0, -1, State.GameBoard);
                    break;
                case Direction.Down:
                    moved = State.Player.TryMove(0, 1, State.GameBoard);
                    break;
            }

            if (moved)
            {
                if (State.IsWithinBounds(oldX, oldY))
                {
                    State.GameBoard[oldY, oldX] = 1;
                }

                CheckDotCollection();

                if (State.IsWithinBounds(State.Player.X, State.Player.Y) &&
                    State.GameBoard[State.Player.Y, State.Player.X] != 5)
                {
                    State.GameBoard[State.Player.Y, State.Player.X] = 5;
                }
            }
        }

        private void CheckDotCollection()
        {
            if (!State.IsWithinBounds(State.Player.X, State.Player.Y)) return;

            int cellValue = State.GameBoard[State.Player.Y, State.Player.X];

            if (cellValue < 0)
            {
                int originalValue = -cellValue;
                if (originalValue == 2)
                {
                    State.Score += 10;
                    State.CollectedDots++;
                    State.GameBoard[State.Player.Y, State.Player.X] = 5;
                }
                else if (originalValue == 3)
                {
                    State.Score += 50;
                    State.CollectedDots++;
                    State.GameBoard[State.Player.Y, State.Player.X] = 5;
                }
            }
            else if (cellValue == 2)
            {
                State.Score += 10;
                State.CollectedDots++;
                State.GameBoard[State.Player.Y, State.Player.X] = 5;
            }
            else if (cellValue == 3)
            {
                State.Score += 50;
                State.CollectedDots++;
                State.GameBoard[State.Player.Y, State.Player.X] = 5;
            }
        }

        private void MoveGhosts()
        {
            RestorePointsUnderGhosts();

            foreach (var ghost in State.Ghosts)
            {
                if (State.IsWithinBounds(ghost.X, ghost.Y) && State.GameBoard[ghost.Y, ghost.X] == 4)
                {
                    State.GameBoard[ghost.Y, ghost.X] = 1;
                }
            }

            foreach (var ghost in State.Ghosts)
            {
                ghost.Move(State.GameBoard, State.Player);

                if (State.IsWithinBounds(ghost.X, ghost.Y))
                {
                    int previousCellValue = State.GameBoard[ghost.Y, ghost.X];
                    if (previousCellValue == 2 || previousCellValue == 3)
                    {
                        State.GameBoard[ghost.Y, ghost.X] = -previousCellValue;
                    }
                    else
                    {
                        State.GameBoard[ghost.Y, ghost.X] = 4;
                    }
                }
            }
        }

        private void RestorePointsUnderGhosts()
        {
            foreach (var ghost in State.Ghosts)
            {
                if (State.IsWithinBounds(ghost.X, ghost.Y) && State.GameBoard[ghost.Y, ghost.X] < 0)
                {
                    int cellValue = State.GameBoard[ghost.Y, ghost.X];
                    State.GameBoard[ghost.Y, ghost.X] = -cellValue;
                }
            }
        }

        private void CheckGhostCollisions()
        {
            foreach (var ghost in State.Ghosts)
            {
                if (ghost.X == State.Player.X && ghost.Y == State.Player.Y)
                {
                    State.Lives--;
                    if (State.Lives <= 0)
                    {
                        State.GameRunning = false;
                        State.IsGameOver = true;
                    }
                    else
                    {
                        if (State.IsWithinBounds(State.Player.X, State.Player.Y))
                            State.GameBoard[State.Player.Y, State.Player.X] = 1;

                        RestorePointsUnderGhosts();
                        ResetPlayer();
                    }
                    break;
                }
            }
        }

        private void ResetPlayer()
        {
            State.Player = new PacmanPlayer(1, 1, GameState.TILE_SIZE - 4);
            if (State.IsWithinBounds(State.Player.X, State.Player.Y))
                State.GameBoard[State.Player.Y, State.Player.X] = 5;
        }

        private void CheckGameEnd()
        {
            if (State.CollectedDots >= State.TotalDots)
            {
                State.GameRunning = false;
                State.IsGameOver = true;
            }
        }

        public void Restart()
        {
            State.Reset();
            _ghostMoveCounter = 0;
        }
    }
}