using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow(string playerName = "Капитан")
        {
            InitializeComponent();

            // Создаём ViewModel и назначаем её как DataContext
            _vm = new MainWindowViewModel();
            DataContext = _vm;

            // Передаём имя игрока
            _vm.PlayerName = playerName;
            this.Title = $"Морской бой — {playerName}";
        }

        private void EnemyCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is CellVM cell)
            {
                // Запрещаем стрелять по уже открытым клеткам
                if (cell.Model.State != CellState.Empty && cell.Model.State != CellState.Ship)
                    return;

                _vm.ShootAtEnemy(cell.X, cell.Y);
            }
        }
    }
}