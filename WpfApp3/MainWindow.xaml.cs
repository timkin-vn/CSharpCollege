using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MazeGame
{
    public partial class MainWindow : Window
    {
        private const int MAZE_SIZE = 15;
        private CellType[,] maze;
        private int playerRow, playerCol;
        private int exitRow, exitCol;
        private int movesCount;
        private bool gameFinished = false;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            Focus(); 
        }

        private void InitializeGame()
        {
            maze = new CellType[MAZE_SIZE, MAZE_SIZE];
            movesCount = 0;
            gameFinished = false;

            GenerateMaze();
            PlacePlayerAndExit();
            UpdateDisplay();
            UpdateStatus();
        }

        private void GenerateMaze()
        {
    
            for (int i = 0; i < MAZE_SIZE; i++)
            {
                for (int j = 0; j < MAZE_SIZE; j++)
                {
                    maze[i, j] = CellType.Wall;
                }
            }


            GenerateMazeDFS(1, 1);


            for (int i = 0; i < MAZE_SIZE; i++)
            {
                maze[i, 0] = CellType.Wall;
                maze[i, MAZE_SIZE - 1] = CellType.Wall;
                maze[0, i] = CellType.Wall;
                maze[MAZE_SIZE - 1, i] = CellType.Wall;
            }

            // Размещаем выход
            exitRow = MAZE_SIZE - 2;
            exitCol = MAZE_SIZE - 2;
            maze[exitRow, exitCol] = CellType.Exit;



        }

        private void GenerateMazeDFS(int row, int col)
        {
            maze[row, col] = CellType.Path;


            var directions = new List<(int, int)> { (0, 2), (2, 0), (0, -2), (-2, 0) };
            Shuffle(directions);

            foreach (var (dr, dc) in directions)
            {
                int newRow = row + dr;
                int newCol = col + dc;

                if (newRow > 0 && newRow < MAZE_SIZE - 1 &&
                    newCol > 0 && newCol < MAZE_SIZE - 1 &&
                    maze[newRow, newCol] == CellType.Wall)
                {

                    maze[row + dr / 2, col + dc / 2] = CellType.Path;
                    GenerateMazeDFS(newRow, newCol);
                }
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
        private void PlacePlayerAndExit()
        {
            playerRow = 1;
            playerCol = 1;
            exitRow = MAZE_SIZE - 2;
            exitCol = MAZE_SIZE - 2;
        }

        private void UpdateDisplay()
        {
            var cells = new List<MazeCellViewModel>();

            for (int i = 0; i < MAZE_SIZE; i++)
            {
                for (int j = 0; j < MAZE_SIZE; j++)
                {
                    var cell = new MazeCellViewModel();

                    if (i == playerRow && j == playerCol)
                    {

                        cell.Symbol = "●";
                        cell.Background = Brushes.LightGreen;
                        cell.Foreground = Brushes.DarkGreen;
                    }
                    else if (i == exitRow && j == exitCol)
                    {

                        cell.Symbol = "★";
                        cell.Background = Brushes.Gold;
                        cell.Foreground = Brushes.DarkOrange;
                    }
                    else
                    {

                        switch (maze[i, j])
                        {
                            case CellType.Wall:
                                cell.Symbol = "█";
                                cell.Background = Brushes.DarkSlateGray;
                                cell.Foreground = Brushes.White;
                                break;
                            case CellType.Path:
                                cell.Symbol = " ";
                                cell.Background = Brushes.White;
                                cell.Foreground = Brushes.Black;
                                break;
                            case CellType.Exit:
                                cell.Symbol = "★";
                                cell.Background = Brushes.Gold;
                                cell.Foreground = Brushes.DarkOrange;
                                break;
                        }
                    }

                    cells.Add(cell);
                }
            }

            GameBoard.ItemsSource = cells;
            MovesText.Text = $"Ходы: {movesCount}";
        }

        private void UpdateStatus()
        {
            if (gameFinished)
            {
                StatusText.Text = $"🎉 Поздравляем! Вы прошли лабиринт за {movesCount} ходов! 🎉";
                StatusText.Foreground = Brushes.DarkGreen;
            }
            else
            {
                StatusText.Text = "Используйте стрелки для движения. Дойдите до выхода (★)!";
                StatusText.Foreground = Brushes.DarkBlue;
            }
        }

        private void MovePlayer(int deltaRow, int deltaCol)
        {
            if (gameFinished) return;

            int newRow = playerRow + deltaRow;
            int newCol = playerCol + deltaCol;


            if (newRow >= 0 && newRow < MAZE_SIZE &&
                newCol >= 0 && newCol < MAZE_SIZE &&
                maze[newRow, newCol] != CellType.Wall)
            {
                playerRow = newRow;
                playerCol = newCol;
                movesCount++;


                if (playerRow == exitRow && playerCol == exitCol)
                {
                    gameFinished = true;
                }

                UpdateDisplay();
                UpdateStatus();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    MovePlayer(-1, 0);
                    break;
                case Key.Down:
                case Key.S:
                    MovePlayer(1, 0);
                    break;
                case Key.Left:
                case Key.A:
                    MovePlayer(0, -1);
                    break;
                case Key.Right:
                case Key.D:
                    MovePlayer(0, 1);
                    break;
                case Key.R:

                    InitializeGame();
                    break;
            }
        }

        private void DirectionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Tag.ToString())
            {
                case "Up":
                    MovePlayer(-1, 0);
                    break;
                case "Down":
                    MovePlayer(1, 0);
                    break;
                case "Left":
                    MovePlayer(0, -1);
                    break;
                case "Right":
                    MovePlayer(0, 1);
                    break;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            Focus(); 
        }
    }


    public class MazeCellViewModel
    {
        public string Symbol { get; set; } = "";
        public Brush Background { get; set; } = Brushes.White;
        public Brush Foreground { get; set; } = Brushes.Black;
    }


    public enum CellType
    {
        Wall,
        Path,
        Exit
    }
}