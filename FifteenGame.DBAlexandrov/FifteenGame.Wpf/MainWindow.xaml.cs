using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MoveDirection moveDirection;
        private Units model;
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
        private List<Units> units = new List<Units>
        {
            new Units(" ", 0, 0, 0, 0, Units.UnitType.None),
            new Units("Д", 100, 20, 0, 1, Units.UnitType.Dragon), // Дракон
            new Units("М", 80, 10, 0, 3, Units.UnitType.Medic), // Медик
            new Units("Р", 120, 25, 0, 5, Units.UnitType.Knight), // Рыцарь
            new Units("К", 150, 15, 0, 7, Units.UnitType.King), // Король
            new Units("Б", 500, 40, 4, 2, Units.UnitType.Boss) // Босс
        };
        private Units boss;
        private Units selectedUnit;
        private GameService gameService;
        
        public MainWindow()
        {
            InitializeComponent();
            
            // Инициализируем сервис игры
            gameService = new GameService();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (FrameworkElement)sender;
            var cellViewModel = grid.DataContext as CellViewModel;
            
            if (cellViewModel != null)
            {
                // Определяем направление хода на основе позиции клетки
                MoveDirection direction = MoveDirection.None;
                
                // Находим пустую клетку
                var emptyCell = ViewModel.Cells.FirstOrDefault(c => c.Num.Type == Units.UnitType.None);
                if (emptyCell != null)
                {
                    // Определяем направление хода
                    if (cellViewModel.Row == emptyCell.Row)
                    {
                        if (cellViewModel.Column == emptyCell.Column - 1)
                            direction = MoveDirection.XRight;
                        else if (cellViewModel.Column == emptyCell.Column + 1)
                            direction = MoveDirection.XLeft;
                    }
                    else if (cellViewModel.Column == emptyCell.Column)
                    {
                        if (cellViewModel.Row == emptyCell.Row - 1)
                            direction = MoveDirection.YDown;
                        else if (cellViewModel.Row == emptyCell.Row + 1)
                            direction = MoveDirection.YUp;
                    }
                }
                
                // Выполняем ход
                ViewModel.MakeMove(direction, cellViewModel.Row, cellViewModel.Column, GameFinished);
            }
        }

        private void GameFinished()
        {
            if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == 
                MessageBoxResult.Yes)
            {
                ViewModel.ReInitialize();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            dialog.ShowDialog();
        }

        public void Initialize(Units model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Модель не может быть null.");
            }

            for (int row = 0; row < Units.RowCount; row++)
            {
                for (int column = 0; column < Units.ColumnCount; column++)
                {
                    model[row, column] = new Units(" ", 0, 0, 0, 0, Units.UnitType.None);
                }
            }

            foreach (var unit in units)
            {
                model[unit.X, unit.Y] = unit;

                if (unit.Type == Units.UnitType.Boss)
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
    }
}
