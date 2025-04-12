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
        private ItemsControl itemsControl;
        private Grid[,] gridButtons;
        private const int ROWS = 8;
        private const int COLS = 8;
        
        private ObservableCollection<CellViewModel> items;
        private GameService gameService;

        
        public MainWindow()
        {
            InitializeComponent();

            itemsControl = new ItemsControl();

            items = new ObservableCollection<CellViewModel>
            {
                new CellViewModel { Num = new Units(" ", 0, 0, 0, 0, Units.UnitType.None), Row = 0, Column = 0 },
                new CellViewModel { Num = new Units("Д", 100, 20, 0, 1, Units.UnitType.Dragon), Row = 0, Column = 1 },
                new CellViewModel { Num = new Units("М", 80, 10, 0, 3, Units.UnitType.Medic), Row = 0, Column = 3 },
                new CellViewModel { Num = new Units("Р", 120, 25, 0, 5, Units.UnitType.Knight), Row = 0, Column = 5 },
                new CellViewModel { Num = new Units("К", 150, 15, 0, 7, Units.UnitType.King), Row = 0, Column = 7 },
                new CellViewModel { Num =  new Units("Б", 500, 40, 4, 2, Units.UnitType.Boss), Row = 4, Column = 2 },
            };
            itemsControl.ItemsSource = items;

            foreach (var unit in items)
            {
                var grid = new Grid();
                grid.Tag = unit;

            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (FrameworkElement)sender;
            GameModel tag = grid.Tag as GameModel;
            Units units = grid.Tag as Units;
            if (tag != null)
            {

                ViewModel.MakeMove(moveDirection, units.X, units.Y, GameFinished);
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

            gridButtons = new Grid[8, 8];

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
