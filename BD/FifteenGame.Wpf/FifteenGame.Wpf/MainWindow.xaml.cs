using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;
using FifteenGame.Business.Models;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow(string playerName)
        {
            InitializeComponent();

            _vm = new MainWindowViewModel();
            DataContext = _vm;

            _vm.PlayerName = playerName;
            _vm.LoadBestTime(playerName); // 🔥 ЗАГРУЗКА РЕКОРДА ПРИ ВХОДЕ

            Title = $"Морской бой — {playerName}";
        }

        private void EnemyCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is CellVM cell)
            {
                if (cell.Model.State != CellState.Empty &&
                    cell.Model.State != CellState.Ship)
                    return;

                _vm.ShootAtEnemy(cell.X, cell.Y);
            }
        }
    }
}
