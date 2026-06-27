using System.Windows;
using System.Windows.Input;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Wpf.ViewModels;
using TwentyFortyEight.Wpf.Views;

namespace TwentyFortyEight.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainSceneModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loginWindow = new UserLoginWindow();
            if (loginWindow.DataContext is UserLoginWindowViewModel loginViewModel)
            {
                loginViewModel.MainViewModel = MainSceneModel;
            }
            loginWindow.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MoveDirection? chosenDirection = null;

            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    chosenDirection = MoveDirection.Left;
                    break;

                case Key.Right:
                case Key.D:
                    chosenDirection = MoveDirection.Right;
                    break;

                case Key.Up:
                case Key.W:
                    chosenDirection = MoveDirection.Up;
                    break;

                case Key.Down:
                case Key.S:
                    chosenDirection = MoveDirection.Down;
                    break;
            }

            if (chosenDirection.HasValue)
            {
                MainSceneModel.MakeMove(chosenDirection.Value, HandleGameOver, HandleGameWon);
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var userChoice = MessageBox.Show(
                "Вы действительно хотите сбросить текущую сессию и начать заново?",
                "Подтверждение сброса",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (userChoice == MessageBoxResult.Yes)
            {
                MainSceneModel.NewGame();
            }
        }

        private void HandleGameWon()
        {
            MessageBox.Show(
                "Цель достигнута! Плитка 2048 собрана.\n\nВы можете продолжить устанавливать новые рекорды!",
                "Успех!",
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
        }

        private void HandleGameOver()
        {
            var choice = MessageBox.Show(
                $"Доступные ходы исчерпаны!\n\nФинальный счёт: {MainSceneModel.Score}\nРекордная плитка: {MainSceneModel.BestTile}\n\nПопробовать снова?",
                "Раунд завершен",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                MainSceneModel.NewGame();
            }
        }
    }
}