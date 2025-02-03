using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ItemsControl itemsControl;
        private Grid[,] gridButtons;
        private const int ROWS = 8;
        private const int COLS = 8;
        private GameModel model;
        private ObservableCollection<CellViewModel> items;

        public MainWindow()
        {
            
            InitializeComponent();
            


            var myGrid = this.FindName("myGrid") as Grid;
            if (myGrid == null)
            {
                MessageBox.Show("Ошибка: myGrid не найден. Проверьте, что он существует в XAML.");
                return; 
            }
            InitializeGridButtons();
            itemsControl = new ItemsControl();

            


            items = new ObservableCollection<CellViewModel>
            {
                new CellViewModel { Num = new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None), Row = 0, Column = 0 },
                new CellViewModel { Num = new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon), Row = 0, Column = 0 },
                new CellViewModel { Num =new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic), Row = 0, Column = 0 },
                new CellViewModel { Num = new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight), Row = 0, Column = 0 },
                new CellViewModel { Num = new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King), Row = 0, Column = 0 },
                new CellViewModel { Num =  new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss), Row = 0, Column = 0 },
            };
            itemsControl.ItemsSource = items;
            CreateGameBoard();
            var units = new List<GameModel>
            {
                new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None),
                new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon), // Дракон
                new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic), // Медик
                new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight), // Рыцарь
                new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King), // Король
                new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss) // Босс
            };

            
            foreach (var unit in units)
            {
                Initialize(unit); 
            }

           
        
        
            
        }


        private void InitializeGridButtons()
        {
            gridButtons = new Grid[ROWS, COLS]; 

            var myGrid = this.FindName("myGrid") as Grid; 
            if (myGrid == null)
            {
                MessageBox.Show("Ошибка: myGrid не найден. Проверьте, что он существует в XAML.");
                return; 
            }
            gridButtons = new Grid[ROWS, COLS];
            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLS; column++)
                {
                    var rectangle = new Grid 
                    {
                        Width = 50,
                        Height = 50,
                        Background = Brushes.Transparent 
                    };

                    rectangle.MouseDown += Grid_MouseDown;

                    myGrid.Children.Add(rectangle); 
                    gridButtons[row, column] = rectangle; 
                }
            }
        }






        public void Initialize(GameModel model)
        {

            gridButtons = new Grid[8, 8];

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
            if (sender is Grid rectangle)
            {
                
                var cellViewModel = rectangle.DataContext as CellViewModel;

                
                if (cellViewModel == null)
                {
                    MessageBox.Show("Ошибка: DataContext не установлен или не является CellViewModel.");
                    return; 
                }

                
                GameModel unit = cellViewModel.Num;
                if (unit != null)
                {
                    MessageBox.Show($"Вы нажали на юнит: {unit.Symbol}");
                    ShowPossibleMoves(unit);
                }
                else
                {
                    MessageBox.Show("Ошибка: Юнит не инициализирован.");
                }
            }
        }





        private void UpdateGameBoard()
        {
            foreach (var unit in units)
            {
                var button = gridButtons[unit.Y, unit.X];
                button.DataContext = unit;
                button.Width = 500;
            }
        }




        private bool IsValidMove(int x, int y)
        {

            if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
                return false;


            return true;
        }

        private void ShowPossibleMoves(GameModel unit)
        {
            
            
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int newX = unit.X + dx;
                    int newY = unit.Y + dy;

                    if (IsValidMove(newX, newY))
                    {
                        
                        gridButtons[newY, newX].Visibility = (Visibility)Opacity; ; 

                       
                        gridButtons[newY, newX].MouseDown += (s, e) =>
                        {
                            
                            unit.X = newX;
                            unit.Y = newY;

                            
                            UpdateGameBoard();
                        };
                    }
                }
            }
        }

        private void CreateGameBoard()
        {

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLS; column++)
                {
                    var gridButtons = new Grid
                    {
                        Width = 50,
                        Height = 50,
                    };

                    
                    for (int x = 0; x < items.Count; x++)
                    {
                        var unit = items[x].Num;
                        if (unit.Symbol != " ") 
                        {
                            gridButtons.DataContext = unit; 
                        }

                        gridButtons.MouseDown += Grid_MouseDown; 

                    }
                }
            }




        }
    }
}
