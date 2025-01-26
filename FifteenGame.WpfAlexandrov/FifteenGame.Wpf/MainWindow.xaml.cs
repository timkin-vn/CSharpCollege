using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FifteenGame.Business.Models.GameModel;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MoveDirection moveDirection;
        private List<GameModel> units;
        private GameModel boss;
        private GameModel selectedUnit;
        private Button[,] gridButtons;
        private const int ROWS = 8;
        private const int COLS = 10;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            var units = new List<GameModel>
            {
                new GameModel("Д", 100, 20, 0, 1, UnitType.Dragon),  // Дракон
                new GameModel("М", 80, 10, 0, 3, UnitType.Medic),    // Медик
                new GameModel("Р", 120, 25, 0, 5, UnitType.Knight),  // Рыцарь
                new GameModel("К", 150, 15, 0, 7, UnitType.King),    // Король
                new GameModel("Б", 500, 40, 6, 2, UnitType.Boss)     // Босс
            };
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.DataContext is GameModel unit)
            {
                
                if (DataContext is MainWindowViewModel viewModel)
                {
                    
                    viewModel.SelectUnit(unit);
                }
            }
        }

        private void CreateGameBoard()
        {
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    var button = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(1),
                        Content = "",
                        Tag = new Point(x, y)
                    };

                    button.Click += GridButton_Click;
                    gridButtons[y, x] = button;
                    GameBoard.Children.Add(button);
                }
            }

            UpdateGameBoard();
        }

        private void UpdateGameBoard()
        {
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    gridButtons[y, x].Content = "";
                    gridButtons[y, x].Background = Brushes.LightGray;
                }
            }

           
            foreach (var unit in units)
            {
                var button = gridButtons[unit.Y, unit.X];
                button.Content = unit.Symbol;
                if (unit.IsSelected)
                {
                    button.Background = Brushes.LightGreen;
                    ShowPossibleMoves(unit);
                }
            }

            for (int y = 2; y <= 5; y++)
            {
                for (int x = 7; x <= 8; x++)
                {
                    gridButtons[y, x].Content = boss.Symbol;
                    gridButtons[y, x].Background = Brushes.LightPink;
                }
            }
        }

        private void ShowPossibleMoves(GameModel gameModel)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int newX = gameModel.X + dx;
                    int newY = gameModel.Y + dy;

                    if (IsValidMove(newX, newY))
                    {
                        gridButtons[newY, newX].Background = Brushes.LightBlue;
                    }
                }
            }
        }

        private bool IsValidMove(int x, int y)
        {
            
            if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
                return false;

            if (units.Any(u => u.X == x && u.Y == y))
                return false;

            if (x >= 7 && x <= 8 && y >= 2 && y <= 5)
                return false;

            return true;
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;
            int x = (int)position.X;
            int y = (int)position.Y;

            if (selectedUnit != null)
            {
                if (IsValidMove(x, y))
                {
                    selectedUnit.X = x;
                    selectedUnit.Y = y;
                    selectedUnit.IsSelected = false;
                    selectedUnit = null;
                    UpdateGameBoard();
                }
                return;
            }

            var clickedUnit = units.FirstOrDefault(u => u.X == x && u.Y == y);
            if (clickedUnit != null)
            {
                if (selectedUnit != null)
                    selectedUnit.IsSelected = false;

                clickedUnit.IsSelected = true;
                selectedUnit = clickedUnit;
                UpdateGameBoard();
            }
        }
    }
}
