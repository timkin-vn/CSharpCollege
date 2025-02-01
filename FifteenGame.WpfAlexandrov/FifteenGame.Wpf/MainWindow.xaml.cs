using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
        private const int COLS = 8;
        private GameModel model;
        public MainWindow()
        {

            InitializeComponent();
            units = new List<GameModel>();
            boss = new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss);
            gridButtons = new Button[ROWS, COLS];
            CreateGameBoard();
            model = new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None); 
            Initialize(model);
            ShowPossibleMoves(model);
            
        }
        private void CreateGameBoard()
        {
            

            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    var button = new Button
                    {
                        Width = 10,
                        Height = 10,
                        Background = new SolidColorBrush(Colors.Black),
                        Content = "hi",
                        Tag = new Point(x, y)
                    };

                    button.Click += GridButton_Click;
                    gridButtons[y, x] = button;
                    GameBoard.Children.Add(button);
                }
            }

            UpdateGameBoard();
        }
        public void Initialize(GameModel model)
        {



            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Модель не может быть null.");
            }

            var units = new List<GameModel>
            {
                new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None),     //Nothing
                new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon),  // Дракон
                new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic),    // Медик
                new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight),  // Рыцарь
                new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King),    // Король
                new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss)     // Босс
            };





            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None);
                }
            }

            foreach (var unit in units)
            {
                model[unit.X, unit.Y] = unit;


                if (unit.Type == GameModel.UnitType.Boss)
                {
                    for (int i = 0; i < unit.Height; i++)
                    {
                        for (int j = 0; j < unit.Width; j++)
                        {
                            model[unit.X + i, unit.Y + j] = unit;
                        }
                    }
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (sender is Grid grid && grid.DataContext is GameModel unit) 
            {
               
                var clickedUnit = units.FirstOrDefault(u => u.X == unit.X && u.Y == unit.Y); 
                if (clickedUnit != null)
                {
                    MessageBox.Show("Hello");
                    if (selectedUnit != null)
                        selectedUnit.IsSelected = false; 

                    clickedUnit.IsSelected = true; 
                    selectedUnit = clickedUnit; 
                    ShowPossibleMoves(clickedUnit); 
                    UpdateGameBoard(); 
                }
            }
        }



        private void UpdateGameBoard()
        {
            var units = new List<GameModel>
            {
                new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None),     //Nothing
                new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon),  // Дракон
                new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic),    // Медик
                new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight),  // Рыцарь
                new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King),    // Король
                new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss)     // Босс
            };
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    gridButtons[y, x].Content = " ";
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

            
        }

        private void ShowPossibleMoves(GameModel gameModel)
        {
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    UpdateGameBoard();
                }
            }

            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) == 1)
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
        }

        private bool IsValidMove(int x, int y)
        {
            
            if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
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
                    // Перемещаем юнита
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
                ShowPossibleMoves(clickedUnit); 
                UpdateGameBoard(); 
            }
        }
    }
}
