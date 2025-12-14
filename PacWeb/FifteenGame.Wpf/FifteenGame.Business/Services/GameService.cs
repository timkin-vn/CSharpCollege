
using StepByStepPacman.Business.Models;
using System;
using System.Linq;

namespace StepByStepPacman.Business.Services
{
    public class GameService
    {
        public GameState State { get; private set; }
        private int _ghostMoveCounter = 0;
        private const int GHOST_MOVE_DELAY = 8;

        public GameService()
        {
            State = new GameState();
        }

        public void Update()
        {
            if (!State.GameRunning) return;

            MovePlayer(State.Player.Direction);

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
            int dx = 0, dy = 0;

            
            switch (direction)
            {
                case Direction.Left: dx = -1; break;
                case Direction.Right: dx = 1; break;
                case Direction.Up: dy = -1; break;
                case Direction.Down: dy = 1; break;
                default: return; 
            }

            
            moved = State.Player.TryMove(dx, dy, State.GameBoard);

            if (moved)
            {
                
                if (State.IsWithinBounds(oldX, oldY))
                {
                    State.GameBoard[oldY, oldX] = 1; 
                }

                CheckDotCollection();

                
                if (State.IsWithinBounds(State.Player.X, State.Player.Y))
                {
                    State.GameBoard[State.Player.Y, State.Player.X] = 5;
                }
            }
        }

        private void CheckDotCollection()
        {
            if (!State.IsWithinBounds(State.Player.X, State.Player.Y)) return;

            int cellValue = State.GameBoard[State.Player.Y, State.Player.X];

            
            if (cellValue == 2 || cellValue == 3)
            {
                State.Score += (cellValue == 2 ? 10 : 50);
                State.CollectedDots++;
            }
            
            else if (cellValue == -2 || cellValue == -3)
            {
                int originalValue = -cellValue;
                State.Score += (originalValue == 2 ? 10 : 50);
                State.CollectedDots++;
            }

           
        }

        private void MoveGhosts()
        {
            
            RestorePointsUnderGhosts();

            
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
                    else if (previousCellValue != 5) 
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
                
                if (State.IsWithinBounds(ghost.X, ghost.Y))
                {
                    int cellValue = State.GameBoard[ghost.Y, ghost.X];

                    
                    if (cellValue < 0)
                    {
                        State.GameBoard[ghost.Y, ghost.X] = -cellValue;
                    }
                    
                    else if (cellValue == 4)
                    {
                        State.GameBoard[ghost.Y, ghost.X] = 1; 
                    }
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
                        ResetPlayerAndGhosts();
                    }
                    break;
                }
            }
        }

        private void ResetPlayerAndGhosts()
        {
            
            State.Player.X = 1;
            State.Player.Y = 1;
            State.Player.Direction = Direction.None;
            if (State.IsWithinBounds(State.Player.X, State.Player.Y))
                State.GameBoard[State.Player.Y, State.Player.X] = 5;

            State.Ghosts[0].X = 9; State.Ghosts[0].Y = 8;
            State.Ghosts[1].X = 8; State.Ghosts[1].Y = 9;
            State.Ghosts[2].X = 9; State.Ghosts[2].Y = 9;
            State.Ghosts[3].X = 10; State.Ghosts[3].Y = 9;
        }

        private void CheckGameEnd()
        {
            if (State.CollectedDots >= State.TotalDots)
            {
                State.GameRunning = false;
                State.IsGameOver = true;
                State.Won = true; 
            }
        }

        public void Restart()
        {
            State.Reset();
            _ghostMoveCounter = 0;
        }
    }
}