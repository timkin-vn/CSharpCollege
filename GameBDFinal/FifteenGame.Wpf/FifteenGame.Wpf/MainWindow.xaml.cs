using FifteenGame.Business.Models;
using FifteenGame.Data.Repositories; // <-- Подключили
using FifteenGame.Wpf.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;
        private readonly string _username;

        // Репозиторий
        private readonly IUserRepository _userRepository = new UserRepository();

        public MainWindow(string playerName = "Капитан")
        {
            InitializeComponent();

            _username = playerName;

            // Инициализация ViewModel
            _vm = new MainWindowViewModel();
            _vm.PlayerName = playerName;
            DataContext = _vm;

            this.Title = $"Морской бой — {playerName}";

            this.Closing += MainWindow_Closing;
        }

        public void LoadGame(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                _vm.LoadGameFromSave(json);
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                // Получаем JSON текущей игры из ViewModel
                string currentGameState = _vm.GetSerializedGame();

                // Сохраняем состояние через репозиторий
                _userRepository.SaveGameState(_username, currentGameState);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сохранения при выходе: {ex.Message}");
            }
        }

        private void EnemyCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is CellVM cell)
            {
                if (cell.Model.State != CellState.Empty && cell.Model.State != CellState.Ship)
                    return;

                _vm.ShootAtEnemy(cell.X, cell.Y);
            }
        }
    }
}