using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MIneSweepper.Bisiness.Model;
using MIneSweepper.Bisiness.Services;
using mineswepperWPF.ViewModel;
namespace mineswepperWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public ViewMineService ViewModel => (ViewMineService)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewMineService(); // Убедитесь, что DataContext установлен
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
{
    var grid = sender as Grid;
    if (grid != null)
    {
        var cellViewModel = grid.DataContext as CellWiewModel;
        if (cellViewModel != null)
        {
            // Здесь вы можете выполнить нужные действия с cellViewModel
            // Например, сделать ход в игре
            int row = cellViewModel.Row;
            int column = cellViewModel.Column;

            // Вызовите метод для обработки хода
            (DataContext as ViewMineService)?.MakeMove(row, column, () =>
            {
                // Действия при завершении игры
                if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                MessageBoxResult.Yes)
                {
                    ViewModel.Initialize();
                }
            });
        }
    }
}

        private void CellFlag(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var cellViewModel = grid.DataContext as CellWiewModel;
                if (cellViewModel != null)
                {
                    // Здесь вы можете выполнить нужные действия с cellViewModel
                    // Например, сделать ход в игре
                    int row = cellViewModel.Row;
                    int column = cellViewModel.Column; ;

                   
                    ViewModel.setFlagCell(row, column);
                   
                }
            }

        }

       

    } } 

