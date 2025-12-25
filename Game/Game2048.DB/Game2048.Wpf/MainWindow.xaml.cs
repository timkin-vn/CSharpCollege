using System.Windows;
using Game2048.Common.Models;
using Game2048.Wpf.ViewModels;

namespace Game2048.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = DataContext as MainWindowViewModel;
            
            // Показать диалог ввода имени при запуске
            ShowPlayerNameDialog();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.W:
                    _viewModel?.Move(MoveDirection.Up);
                    break;
                case System.Windows.Input.Key.S:
                    _viewModel?.Move(MoveDirection.Down);
                    break;
                case System.Windows.Input.Key.A:
                    _viewModel?.Move(MoveDirection.Left);
                    break;
                case System.Windows.Input.Key.D:
                    _viewModel?.Move(MoveDirection.Right);
                    break;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ShowPlayerNameDialog();
            _viewModel?.StartNewGame();
        }

        private void ShowPlayerNameDialog()
        {
            var dialog = new PlayerNameDialog();
            if (dialog.ShowDialog() == true)
            {
                _viewModel?.SetPlayerName(dialog.PlayerName);
            }
        }
    }
}